(function () {
  "use strict";

  document
    .querySelector("#btnAddArticle")
    .addEventListener("click", function () {
      const articleId = parseInt(document.getElementById("articleId").value);
      const articleQty = parseInt(document.getElementById("articleQty").value);
      const articlePrice = parseFloat(
        document.getElementById("articlePrice").value
      );

      if (
        isNaN(articleId) ||
        isNaN(articleQty) ||
        isNaN(articlePrice) ||
        articleQty <= 0 ||
        articlePrice <= 0
      ) {
        Swal.fire({
          icon: "warning",
          title: "Artículos",
          text: "El código de artículo, cantidad y precio son obligatorios y deben ser válidos.",
          confirmButtonText: "OK",
          confirmButtonColor: "#3085d6",
        });
        return;
      }

      const article = {
        ArticleId: articleId,
        Quantity: articleQty,
        Price: articlePrice,
      };

      articles.push(article);
      updateArticleTable(); // Actualizar tabla de artículos
      updateTotals(); // Actualizar totales de la factura
    });
})();

function updateArticleTable() {
  const tableBody = document.getElementById("articlesTable");
  tableBody.innerHTML = ""; // Limpiar tabla

  if (articles.length === 0) {
    // Mostrar mensaje vacío
    tableBody.innerHTML = `
        <tr class="empty-row">
          <td colspan="5" class="text-center text-muted">
            No hay artículos agregados.
          </td>
        </tr>`;
    return;
  }

  // Agregar filas con los artículos
  articles.forEach((article, index) => {
    const row = document.createElement("tr");

    row.innerHTML = `
        <td>${index + 1}</td>
        <td>${article.ArticleId}</td>
        <td>${article.Quantity}</td>
        <td>${formatCurrency(article.Price)}</td>
        <td>
          <button class="btn btn-danger btn-sm" onclick="removeArticle(${index})">
            <span data-feather="trash-2"></span>
          </button>
        </td>
      `;

    tableBody.appendChild(row);
  });

  document.getElementById("articleId").value = "";
  document.getElementById("articleQty").value = "";
  document.getElementById("articlePrice").value = "";

  // Actualizar íconos feather si los estás usando
  if (window.feather) feather.replace();
}

function removeArticle(index) {
  articles.splice(index, 1); // Eliminar 1 artículo en la posición index
  updateArticleTable(); // Volver a renderizar la tabla
  updateTotals(); // Actualizar totales de la factura
}

async function createInvoice(invoiceData) {
  try {
    const response = await fetch("/Invoice/Create", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: invoiceData,
    });

    if (response.ok) {
      resetForm(); // Reiniciar el formulario y los artículos

      const result = await response.json();

      Swal.fire({
        icon: "success",
        title: "¡Factura creada con éxito!",
        html: `
          <p>Se ha creado la factura con el ID: <strong>${result.invoiceId}</strong></p>
          <p>¿Deseas crear una nueva factura?</p>
        `,
        confirmButtonText: "Sí, crear otra factura",
        confirmButtonColor: "#3085d6",
        showCancelButton: true,
        cancelButtonText: "No, volver a la lista",
        cancelButtonColor: "#d33",
      }).then((result) => {
        if (!result.isConfirmed) {
          window.location.href = "/Invoices";
        }
      });
    } else {
      throw new Error("Error al crear la factura.");
    }
  } catch (error) {
    Swal.fire({
      icon: "error",
      title: "Error",
      text: error.message,
      confirmButtonText: "OK",
      confirmButtonColor: "#d33",
    });
  }
}
