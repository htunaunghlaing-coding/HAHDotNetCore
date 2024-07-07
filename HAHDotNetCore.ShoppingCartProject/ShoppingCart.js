const tblProduct = "products";
let productId = null;

getProductsTable();
updateCartCount();

function createProduct(name, category, price) {
  let list = getProducts();

  const requestModel = {
    id: uuidv4(),
    name: name,
    category: category,
    price: price,
  };

  list.push(requestModel);

  const jsonProduct = JSON.stringify(list);
  localStorage.setItem(tblProduct, jsonProduct);

  successMessage("Product Created Successfully.");
  clearControl();
}

function editProduct(id) {
  let list = getProducts();

  const items = list.filter((x) => x.id === id);
  console.log(items);

  if (items.length === 0) {
    errorMessage("No data found in the table.");
    return;
  }

  let item = items[0];
  productId = item.id;
  $("#txtProductName").val(item.name);
  $("#txtCategory").val(item.category);
  $("#txtPrice").val(item.price);
  $("#txtProductName").focus();
}

function updateProduct(id, name, category, price) {
  let list = getProducts();

  const items = list.filter((x) => x.id === id);
  console.log(items);

  if (items.length === 0) {
    errorMessage("No data found in the table.");
    return;
  }

  const item = items[0];
  item.name = name;
  item.category = category;
  item.price = price;

  const index = list.findIndex((x) => x.id === id);
  list[index] = item;

  const jsonProduct = JSON.stringify(list);
  localStorage.setItem(tblProduct, jsonProduct);

  successMessage("Product Updated Successfully.");
}

function deleteProduct(id) {
  let result = confirm("Are you sure you want to delete this product?");

  if (!result) return;

  let list = getProducts();

  list = list.filter((x) => x.id !== id);
  const jsonProduct = JSON.stringify(list);
  localStorage.setItem(tblProduct, jsonProduct);

  successMessage("Product Deleted Successfully.");
  getProductsTable();
}

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      +c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (+c / 4)))
    ).toString(16)
  );
}

function getProducts() {
  const products = localStorage.getItem(tblProduct);
  let list = [];
  if (products !== null) {
    list = JSON.parse(products);
  }
  return list;
}

$("#btnSave").click(function () {
  const name = $("#txtProductName").val().trim();
  const category = $("#txtCategory").val().trim();
  const price = $("#txtPrice").val().trim();

  $(".form-control").removeClass("warning");
  $(".warning-message").remove();

  let hasError = false;

  if (!name) {
    console.log("Product Name is empty");
    $("#txtProductName").addClass("warning");
    $("#txtProductName").after(
      '<div class="warning-message">Product Name is required.</div>'
    );
    hasError = true;
  }

  if (!category) {
    console.log("Product Category is empty");
    $("#txtCategory").addClass("warning");
    $("#txtCategory").after(
      '<div class="warning-message">Product Category is required.</div>'
    );
    hasError = true;
  }

  if (!price) {
    console.log("Product Price is empty");
    $("#txtPrice").addClass("warning");
    $("#txtPrice").after(
      '<div class="warning-message">Product Price is required.</div>'
    );
    hasError = true;
  }

  if (hasError) {
    return;
  }

  if (productId === null) {
    createProduct(name, category, price);
  } else {
    updateProduct(productId, name, category, price);
    productId = null;
  }

  getProductsTable();
  clearControl();
});

$("#btnCancel").click(function () {
  clearControl();
});

function successMessage(message) {
  alert(message);
}

function errorMessage(message) {
  alert(message);
}

function clearControl() {
  $("#txtProductName").val("");
  $("#txtCategory").val("");
  $("#txtPrice").val("");
  $("#txtProductName").focus();
  productId = null;

  $(".form-control").removeClass("warning");
  $(".warning-message").remove();
}

function getProductsTable() {
  const list = getProducts();
  let count = 0;
  let htmlRows = "";
  list.forEach((item) => {
    const htmlRow = `
                <tr>
                    <td>${++count}</td>
                    <td>${item.name}</td>
                    <td>${item.category}</td>
                    <td>${item.price}</td>
                    <td>
                        <button type="button" class="btn btn-warning btn-sm me-4" onclick="editProduct('${
                          item.id
                        }')">
                            <i class="fa fa-edit"></i> Edit
                        </button>
                        <button type="button" class="btn btn-danger btn-sm me-4" onclick="deleteProduct('${
                          item.id
                        }')">
                            <i class="fa fa-trash"></i> Delete
                        </button>
                        <button type="button" class="btn btn-primary btn-sm " onclick="addToCart('${
                          item.id
                        }')">
                            <i class="fa-solid fa-plus"></i> Add
                        </button>
                    </td>
                </tr>
            `;
    htmlRows += htmlRow;
  });
  $("#tbody").html(htmlRows);
}

function addToCart(id) {
  const list = getProducts();
  const item = list.find((product) => product.id === id);

  if (item) {
    item.count = (item.count || 0) + 1;

    localStorage.setItem(tblProduct, JSON.stringify(list));
    updateCartCount();
    successMessage("Product Added To Cart");
  } else {
    errorMessage("Product not found.");
  }
}

function updateCartCount() {
  const list = getProducts();
  let totalItems = 0;

  list.forEach((item) => {
    totalItems += item.count || 0;
  });

  console.log("Total items in cart:", totalItems);
  $("#cartCount").text(totalItems);
}

$("form").submit(function (event) {
  event.preventDefault();

  const searchTerm = $("#searchInput").val().trim().toLowerCase();

  if (!searchTerm) {
    getProductsTable();
    return;
  }

  const list = getProducts();
  const filteredProducts = list.filter(
    (product) =>
      product.name.toLowerCase().includes(searchTerm) ||
      product.category.toLowerCase().includes(searchTerm) ||
      product.price.toLowerCase().includes(searchTerm)
  );

  displayFilteredProducts(filteredProducts);
});

function displayFilteredProducts(products) {
  let count = 0;
  let htmlRows = "";
  products.forEach((item) => {
    const htmlRow = `
            <tr>
                <td>${++count}</td>
                <td>${item.name}</td>
                <td>${item.category}</td>
                <td>${item.price}</td>
                <td>
                    <button type="button" class="btn btn-warning btn-sm me-4" onclick="editProduct('${
                      item.id
                    }')">
                        <i class="fa fa-edit"></i> Edit
                    </button>
                    <button type="button" class="btn btn-danger btn-sm me-4" onclick="deleteProduct('${
                      item.id
                    }')">
                        <i class="fa fa-trash"></i> Delete
                    </button>
                    <button type="button" class="btn btn-primary btn-sm " onclick="addToCart('${
                      item.id
                    }')">
                        <i class="fa-solid fa-plus"></i> Add
                    </button>
                </td>
            </tr>
        `;
    htmlRows += htmlRow;
  });

  $("#tbody").html(htmlRows);
}
