(function () {
  "use strict";

  document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("invoiceForm");

    if (!form) return;

    form.addEventListener("submit", function (event) {
      let isValid = form.checkValidity();

      if (!isValid) {
        event.preventDefault();
        event.stopPropagation();
      }

      // Validar si hay al menos un artículo en el array
      if (isValid && articles.length === 0) {
        event.preventDefault();
        event.stopPropagation();

        Swal.fire({
          icon: "warning",
          title: "Artículos",
          text: "No hay artículos en la factura. Debes agregar al menos uno.",
          confirmButtonText: "OK",
          confirmButtonColor: "#3085d6",
        });

        return;
      }

      form.classList.add("was-validated");

      // Si el formulario es válido, se procede a enviar los datos
      if (isValid) {
        event.preventDefault();

        // Se crea un objeto para los datos del formulario
        const formValues = {
          InvoiceDate: document.getElementById("invoiceDate").value,
          CliDocument: document.getElementById("document").value,
          CliNames: document.getElementById("names").value,
          CliLastNames: document.getElementById("lastNames").value,
          CliAddress: document.getElementById("address").value,
          CliPhone: document.getElementById("phone").value,
        };

        // Agregar el array de artículos al objeto formValues
        formValues.Articles = articles;

        // Convertir el objeto formValues a JSON
        const invoiceData = JSON.stringify(formValues);

        // Enviar los datos para crear la factura
        createInvoice(invoiceData);
      }
    });
  });
})();

function resetForm() {
  // Reiniciar el formulario principal de la factura
  const invoiceForm = document.getElementById("invoiceForm");
  if (invoiceForm) {
    invoiceForm.reset();
  }

  // Limpiar el array de artículos
  articles = [];

  // Limpiar la tabla de artículos
  const tableBody = document.getElementById("articlesTable");
  if (tableBody) {
    tableBody.innerHTML = `
      <tr class="empty-row">
        <td colspan="5" class="text-center text-muted">
          No hay artículos agregados.
        </td>
      </tr>`;
  }

  // Reiniciar los totales
  document.getElementById("grossAmount").textContent = "$0";
  document.getElementById("discount").textContent = "$0";
  document.getElementById("tax").textContent = "$0";
  document.getElementById("invoiceTotal").textContent = "$0";
}
