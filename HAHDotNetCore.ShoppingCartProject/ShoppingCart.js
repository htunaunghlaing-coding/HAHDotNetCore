const tblProduct = "products";

getProductsTable();
// createProduct();
// updateProduct(
//   "60f2a5a2-f9df-4051-83ac-3e6b4538d13c",
//   "Cheese Cake",
//   "Bakery",
//   "4000 MMK"
// );
deleteProduct("60f2a5a2-f9df-4051-83ac-3e6b4538d13c");

function readProduct() {
  let list = localStorage.getItem();
  console.log(list);
}

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

function updateProduct(id, name, category, price) {
  let list = getProducts();

  const items = list.filter((x) => x.id === id);
  console.log(items);

  console.log(items.length);

  if (items.length == 0) {
    console.log("Not data found in the table.");
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
}

function deleteProduct(id) {
  let list = getProducts();

  const items = list.filter((x) => x.id === id);
  if (items.length == 0) {
    console.log("No Data Found in the table.");
    return;
  }

  list = list.filter((x) => x.id !== id);
  const jsonProduct = JSON.stringify(list);
  localStorage.setItem(tblProduct, jsonProduct);
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

  createProduct(name, category, price);
});

function successMessage(message) {
  alert(message);
}

function errorMessage(message) {
  alert(message);
}

function clearControl() {
  $("#txtProductName").val(" ");
  $("#txtCategory").val(" ");
  $("#txtPrice").val(" ");
  $("#txtProductName").focus();
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
      </tr>
      `;
    htmlRows += htmlRow;
  });
  $("#tbody").html(htmlRows);
}
