(function () {
  "use strict";

  document.addEventListener("DOMContentLoaded", function () {
    const invoiceDateInput = document.getElementById("invoiceDate");

    if (invoiceDateInput) {
      const now = new Date();

      // Ajustar a hora local y formatear como YYYY-MM-DD
      const localDate = new Date(
        now.getTime() - now.getTimezoneOffset() * 60000
      )
        .toISOString()
        .split("T")[0]; // Tomar solo la parte de la fecha

      invoiceDateInput.value = localDate;
    }
  });
})();

function calculateTotals() {
  // Calcular el valor bruto (suma de cantidad * precio de todos los artículos)
  let grossAmount = 0;
  articles.forEach((article) => {
    grossAmount += article.Quantity * article.Price;
  });

  // Calcular el descuento (5% si el valor bruto es mayor o igual a 500.000)
  let discount = 0;
  if (grossAmount >= 500000) {
    discount = grossAmount * 0.05;
  }

  // Calcular el IVA (19% del valor bruto)
  const tax = grossAmount * 0.19;

  // Calcular el total de la factura (valor bruto + IVA - descuento)
  const invoiceTotal = grossAmount + tax - discount;

  // Mostrar los resultados en los campos de solo lectura
  document.getElementById("grossAmount").textContent =
    formatCurrency(grossAmount);
  document.getElementById("discount").textContent = formatCurrency(discount);
  document.getElementById("tax").textContent = formatCurrency(tax);
  document.getElementById("invoiceTotal").textContent =
    formatCurrency(invoiceTotal);
}

// Función para formatear los valores como moneda
function formatCurrency(value) {
  return value.toLocaleString("es-CO", {
    style: "currency",
    currency: "COP",
    minimumFractionDigits: 0,
    maximumFractionDigits: 0,
  });
}

// Actualiza los totales cuando se agregue un artículo
function updateTotals() {
  calculateTotals();
}
