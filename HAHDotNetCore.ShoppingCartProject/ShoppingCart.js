const tblProduct = "products";
let productId = null;  

getProductsTable();

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

  successMessage("Create A Product Successfully.");
  clearControl();
}

function editProduct(id) {
  let list = getProducts();

  const items = list.filter((x) => x.id === id);
  console.log(items);

  if (items.length == 0) {
    errorMessage("Not data found in the table.");
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

  if (items.length == 0) {
    errorMessage("Not data found in the table.");
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

  successMessage("Update Data Successfully.");
}

function deleteProduct(id) {
  let result = confirm("Are You Sure Want To Delete This Product?.");

  if (!result) return;

  let list = getProducts();

  const items = list.filter((x) => x.id === id);
  if (items.length == 0) {
    console.log("No Data Found in the table.");
    return;
  }

  list = list.filter((x) => x.id !== id);
  const jsonProduct = JSON.stringify(list);
  localStorage.setItem(tblProduct, jsonProduct);

  successMessage("Delete Data Successfully.");
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
  console.log(products);

  let list = [];
  if (products !== null) {
    list = JSON.parse(products);
  }
  return list;
}

$("#btnSave").click(function () {
  const name = $("#txtProductName").val();
  const category = $("#txtCategory").val();
  const price = $("#txtPrice").val();

  if (productId === null) {
    createProduct(name, category, price);
  } else {
    updateProduct(productId, name, category, price);
    productId = null; 
  }

  getProductsTable();
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
            <button type="button" class="btn btn-warning btn-sm me-4" onclick="editProduct('${item.id}')">
              <i class="fa fa-edit"></i> Edit
            </button>
            <button type="button" class="btn btn-danger btn-sm" onclick="deleteProduct('${item.id}')">
              <i class="fa fa-trash"></i> Delete
            </button>
          </td>
      </tr>
    `;
    htmlRows += htmlRow;
  });
  $("#tbody").html(htmlRows);
}
