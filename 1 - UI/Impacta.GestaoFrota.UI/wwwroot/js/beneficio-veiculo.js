(() => {
    const modalElement = document.getElementById('beneficioVeiculoModal');
    const modalContent = document.getElementById('beneficioVeiculoModalContent');
    const modal = modalElement ? bootstrap.Modal.getOrCreateInstance(modalElement) : null;

    function atualizarPessoa(input) {
        const form = input.closest('form');
        const idFieldName = input.name === 'BeneficiadoSelecao' ? 'IdBeneficiado' : 'IdMotorista';
        const idField = form?.querySelector(`[name="${idFieldName}"]`);
        const list = input.list;
        const option = list && Array.from(list.options).find(item => item.value === input.value);

        if (idField) idField.value = option?.dataset.pessoaId ?? '0';
        input.setCustomValidity(option ? '' : 'Selecione uma pessoa da lista.');
        return Boolean(option);
    }

    document.addEventListener('input', event => {
        if (event.target.matches('[name="BeneficiadoSelecao"], [name="MotoristaSelecao"]'))
            atualizarPessoa(event.target);
    });

    document.addEventListener('submit', event => {
        const form = event.target;
        if (!form.matches('[data-person-form]')) return;

        const inputs = form.querySelectorAll('[name="BeneficiadoSelecao"], [name="MotoristaSelecao"]');
        const valid = Array.from(inputs).map(atualizarPessoa).every(Boolean);
        if (!valid) {
            event.preventDefault();
            event.stopImmediatePropagation();
            form.querySelector(':invalid')?.reportValidity();
        }
    }, true);

    function mostrarMensagem(tipo, mensagem) {
        const container = document.getElementById('operationMessage');
        if (!container) return;

        const alert = document.createElement('div');
        alert.className = `alert alert-${tipo} alert-dismissible fade show shadow`;
        alert.setAttribute('role', 'alert');
        alert.textContent = mensagem;

        const fechar = document.createElement('button');
        fechar.type = 'button';
        fechar.className = 'btn-close';
        fechar.setAttribute('data-bs-dismiss', 'alert');
        fechar.setAttribute('aria-label', 'Fechar');
        alert.append(fechar);
        container.replaceChildren(alert);
        window.setTimeout(() => bootstrap.Alert.getOrCreateInstance(alert).close(), 4000);
    }

    async function carregarModal(url) {
        modalContent.innerHTML = '<div class="modal-body text-center p-5"><span class="spinner-border text-primary" role="status"></span></div>';
        modal.show();

        const response = await fetch(url, { headers: { 'X-Requested-With': 'XMLHttpRequest' } });
        if (!response.ok) {
            modalContent.innerHTML = '<div class="modal-body"><div class="alert alert-danger mb-0">Não foi possível carregar o formulário.</div></div>';
            return;
        }

        modalContent.innerHTML = await response.text();
        const form = modalContent.querySelector('[data-modal-form]');
        if (form && window.jQuery && $.validator?.unobtrusive)
            $.validator.unobtrusive.parse(form);
    }

    document.addEventListener('click', event => {
        const link = event.target.closest('[data-modal-url]');
        if (!link || !modal) return;
        event.preventDefault();
        carregarModal(link.dataset.modalUrl);
    });

    if (modalContent) {
        modalContent.addEventListener('submit', async event => {
            const form = event.target;
            if (!form.matches('[data-modal-form], [data-delete-form]')) return;
            event.preventDefault();

            const button = form.querySelector('[type="submit"]');
            button.disabled = true;
            try {
                const response = await fetch(form.action, {
                    method: 'POST',
                    body: new FormData(form),
                    headers: { 'X-Requested-With': 'XMLHttpRequest' }
                });
                const result = await response.json();
                if (result.success) {
                    modal.hide();
                    mostrarMensagem('success', result.message);
                    window.setTimeout(() => window.location.reload(), 1200);
                } else {
                    mostrarMensagem('danger', result.message || 'Não foi possível salvar as alterações.');
                    button.disabled = false;
                }
            } catch {
                mostrarMensagem('danger', 'Ocorreu um erro ao processar a solicitação.');
                button.disabled = false;
            }
        });
    }
})();