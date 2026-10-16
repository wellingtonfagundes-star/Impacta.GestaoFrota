(() => {
    const modalElement = document.getElementById('clienteModal');
    const modalContent = document.getElementById('clienteModalContent');
    const modal = bootstrap.Modal.getOrCreateInstance(modalElement);

    if (window.jQuery && $.validator) {
        $.validator.addMethod('documentocliente', (value, element) => {
            if ($.trim(value) === '') return true;
            const form = element.closest('form');
            const tipo = form.querySelector('[data-tipo-cliente]').value;
            return validarDocumento(value, tipo);
        }, 'Informe um CPF ou CNPJ válido para o tipo de cliente selecionado.');
        if ($.validator.unobtrusive) $.validator.unobtrusive.adapters.addBool('documentocliente');
    }

    function validarCpf(value) {
        const cpf = value.replace(/\D/g, '');
        if (!/^\d{11}$/.test(cpf) || /^(\d)\1{10}$/.test(cpf)) return false;

        let soma = 0;
        for (let indice = 0; indice < 9; indice++) soma += Number(cpf[indice]) * (10 - indice);
        let digito = (soma * 10) % 11;
        if (digito === 10) digito = 0;
        if (Number(cpf[9]) !== digito) return false;

        soma = 0;
        for (let indice = 0; indice < 10; indice++) soma += Number(cpf[indice]) * (11 - indice);
        digito = (soma * 10) % 11;
        if (digito === 10) digito = 0;
        return Number(cpf[10]) === digito;
    }

    function validarCnpj(value) {
        const cnpj = value.replace(/[^A-Za-z0-9]/g, '').toUpperCase();
        if (!/^[A-Z0-9]{12}\d{2}$/.test(cnpj) || /^([A-Z0-9])\1{13}$/.test(cnpj)) return false;

        const digitos = [...cnpj].map(caractere => caractere.charCodeAt(0) - 48);
        const calcularDigito = pesos => {
            const soma = pesos.reduce((total, peso, indice) => total + digitos[indice] * peso, 0);
            const resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        };
        const primeiro = calcularDigito([5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);
        if (digitos[12] !== primeiro) return false;
        digitos[12] = primeiro;
        return digitos[13] === calcularDigito([6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);
    }

    function validarDocumento(value, tipo) {
        return tipo === 'PF' ? validarCpf(value) : tipo === 'PJ' ? validarCnpj(value) : false;
    }

    function aplicarMascara(value, tipo) {
        const limite = tipo === 'PJ' ? 14 : 11;
        const digitos = (tipo === 'PJ'
            ? value.replace(/[^A-Za-z0-9]/g, '').toUpperCase()
            : value.replace(/\D/g, '')).slice(0, limite);
        if (tipo === 'PJ') {
            return digitos
                .replace(/^([A-Z0-9]{2})([A-Z0-9])/, '$1.$2')
                .replace(/^([A-Z0-9]{2})\.([A-Z0-9]{3})([A-Z0-9])/, '$1.$2.$3')
                .replace(/\.([A-Z0-9]{3})([A-Z0-9])/, '.$1/$2')
                .replace(/([A-Z0-9]{4})([A-Z0-9])/, '$1-$2');
        }

        return digitos
            .replace(/^(\d{3})(\d)/, '$1.$2')
            .replace(/^(\d{3})\.(\d{3})(\d)/, '$1.$2.$3')
            .replace(/(\d{3})(\d{1,2})$/, '$1-$2');
    }

    document.querySelectorAll('.crud-alert').forEach(alert => setTimeout(() => bootstrap.Alert.getOrCreateInstance(alert).close(), 4000));

    async function loadModal(url) {
        modalContent.innerHTML = '<div class="modal-body text-center p-5"><span class="spinner-border text-primary" role="status"></span></div>';
        modal.show();
        try {
            const response = await fetch(url, { headers: { 'X-Requested-With': 'XMLHttpRequest' } });
            if (!response.ok) {
                modalContent.innerHTML = '<div class="modal-body"><div class="alert alert-danger mb-0">Não foi possível carregar o formulário.</div></div>';
                return;
            }
            modalContent.innerHTML = await response.text();
            bindForm();
            const form = modalContent.querySelector('[data-modal-form]');
            if (form && window.jQuery && $.validator && $.validator.unobtrusive) $.validator.unobtrusive.parse(form);
        } catch {
            modalContent.innerHTML = '<div class="modal-body"><div class="alert alert-danger mb-0">Não foi possível carregar o formulário.</div></div>';
        }
    }

    function showOperationMessage(type, message) {
        const container = document.getElementById('operationMessage');
        container.innerHTML = `<div class="alert alert-${type} alert-dismissible fade show shadow" role="alert"><i class="bi bi-${type === 'success' ? 'check-circle-fill' : 'exclamation-circle-fill'} me-2"></i><span data-message></span><button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Fechar"></button></div>`;
        container.querySelector('[data-message]').textContent = message;
        setTimeout(() => { const alert = container.querySelector('.alert'); if (alert) bootstrap.Alert.getOrCreateInstance(alert).close(); }, 5000);
    }

    function bindForm() {
        const form = modalContent.querySelector('[data-modal-form]');
        const deleteForm = modalContent.querySelector('[data-delete-form]');
        const activeForm = deleteForm || form;
        if (!activeForm) return;

        const tipoInput = activeForm.querySelector('[data-tipo-cliente]');
        const documentoInput = activeForm.querySelector('[data-documento-cliente]');
        const documentoLabel = activeForm.querySelector('[data-documento-label]');

        function atualizarDocumento(limpar) {
            if (!tipoInput || !documentoInput) return;
            const tipo = tipoInput.value;
            const pessoaJuridica = tipo === 'PJ';
            documentoLabel.textContent = pessoaJuridica ? 'CNPJ' : 'CPF';
            documentoInput.placeholder = pessoaJuridica ? '00.000.000/0000-00' : '000.000.000-00';
            documentoInput.maxLength = pessoaJuridica ? 18 : 14;
            documentoInput.value = aplicarMascara(limpar ? '' : documentoInput.value, tipo);
        }

        if (tipoInput && documentoInput) {
            atualizarDocumento(false);
            tipoInput.addEventListener('change', () => atualizarDocumento(true));
            documentoInput.addEventListener('input', () => atualizarDocumento(false));
        }

        activeForm.addEventListener('submit', async event => {
            event.preventDefault();
            if (form && window.jQuery && $.validator && !$(form).valid()) return;
            const button = activeForm.querySelector('button[type="submit"]');
            button.disabled = true;
            try {
                const response = await fetch(activeForm.action, { method: 'POST', body: new FormData(activeForm), headers: { 'X-Requested-With': 'XMLHttpRequest' } });
                const result = await response.json();
                if (result.success) {
                    modal.hide();
                    showOperationMessage('success', result.message);
                    setTimeout(() => window.location.reload(), 2500);
                } else {
                    button.disabled = false;
                    showOperationMessage('danger', result.message);
                }
            } catch {
                button.disabled = false;
                showOperationMessage('danger', 'Não foi possível concluir a operação. Tente novamente.');
            }
        });
    }

    document.querySelectorAll('[data-modal-url]').forEach(link => link.addEventListener('click', event => {
        event.preventDefault();
        loadModal(link.dataset.modalUrl);
    }));
})();
