const tblProduct = "products";

createProduct();

function readProduct() {
  localStorage.getItem();
}

function createProduct() {
  const products = localStorage.getItem(tblProduct);
  console.log(products);

  const requestModel = {
    name: "Mini Croissant",
    category: "Bakery",
    Quantity: "1",
    Price: "5500 MMK",
  };

  let list = [];

  if (products !== null) {
    list = JSON.parse(products);
    console.log(list);
  }

  list.push(requestModel);

  const jsonProduct = JSON.stringify(list);
  localStorage.setItem(tblProduct, jsonProduct);
}
