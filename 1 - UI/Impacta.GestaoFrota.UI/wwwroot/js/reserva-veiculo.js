document.addEventListener("DOMContentLoaded", () => {
    const returnDate = document.querySelector("[data-return-date]");
    const returnToggle = document.querySelector("[data-return-toggle]");
    const returnFields = document.querySelectorAll("[data-return-field]");
    const localPicker = document.querySelector("[data-local-picker]");
    const returnDetails = document.getElementById("returnDetails");
    const localId = document.getElementById("IdLocalRetorno");
    const localName = document.getElementById("LocalRetornoNome");

    if (!returnDate || !returnToggle || !returnDetails) {
        return;
    }

    const setReturnEnabled = (clearFields) => {
        const enabled = !returnDate.disabled && Boolean(returnDate.value);
        returnToggle.disabled = !enabled;
        returnFields.forEach((field) => {
            field.disabled = !enabled;
            if (clearFields && field.type !== "hidden") {
                field.value = "";
            }
        });
        if (localPicker) {
            localPicker.disabled = !enabled;
        }
        if (clearFields) {
            localId.value = "";
            localName.value = "";
        }

        const collapse = bootstrap.Collapse.getOrCreateInstance(returnDetails, { toggle: false });
        if (enabled) {
            collapse.show();
        } else {
            collapse.hide();
        }
    };

    returnDate.addEventListener("change", () => setReturnEnabled(!returnDate.value));
    setReturnEnabled(false);

    document.querySelectorAll("[data-select-local]").forEach((button) => {
        button.addEventListener("click", () => {
            localId.value = button.dataset.id;
            localName.value = button.dataset.name;
            bootstrap.Modal.getOrCreateInstance(document.getElementById("localPickerModal")).hide();
        });
    });
});
