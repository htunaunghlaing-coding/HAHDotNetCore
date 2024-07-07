const tblProduct = "products";

updateTotalAmount();
displayCartItems();

function getProducts() {
  const products = localStorage.getItem(tblProduct);
  let list = [];
  if (products !== null) {
    list = JSON.parse(products);
  }
  return list;
}

function displayCartItems() {
  let list = getProducts();
  let cartItems = list.filter((item) => item.count > 0);

  if (cartItems.length == 0) {
    $("#cardBody").html(
      "<tr><td colspan='6' class='text-center'>Your cart is empty.</td></tr>"
    );
    return;
  }

  let htmlRows = "";
  cartItems.forEach((item) => {
    const subTotal = item.count * parseFloat(item.price);
    const htmlRow = `
         <tr>
            <td>${item.name}</td>
            <td>${item.category}</td>
            <td>${item.price}</td>
            <td class="quantity-control">
               <button class="btn btn-sm btn-secondary" onclick="decreaseQuantity('${item.id}')">-</button>
               <span class="quantity-count">${item.count}</span>
               <button class="btn btn-sm btn-primary" onclick="increaseQuantity('${item.id}')">+</button>
            </td>
            <td>$${subTotal.toFixed(2)}</td>
            <td><button class="btn btn-danger btn-sm" onclick="removeFromCart('${
              item.id
            }')">Remove</button></td>
         </tr>
      `;
    htmlRows += htmlRow;
  });
  $("#cartBody").html(htmlRows);
}

function increaseQuantity(id) {
  let list = getProducts();

  const item = list.find((x) => x.id === id);
  if (item) {
    item.count += 1;
    localStorage.setItem(tblProduct, JSON.stringify(list));
    displayCartItems();
    updateTotalAmount();
  }
}

function decreaseQuantity(id) {
  let list = getProducts();

  const item = list.find((x) => x.id === id);
  if (item && item.count > 1) {
    item.count -= 1;
    localStorage.setItem(tblProduct, JSON.stringify(list));
    displayCartItems();
    updateTotalAmount();
  }
}

function removeFromCart(id) {
  let list = getProducts();

  const item = list.find((x) => x.id === id);
  if (item) {
    item.count = 0;
    localStorage.setItem(tblProduct, JSON.stringify(list));
    displayCartItems();
    updateTotalAmount();
  }
}

function updateTotalAmount() {
  let list = getProducts();
  let total = list.reduce(
    (sum, item) => sum + item.count * parseFloat(item.price),
    0
  );

  $("#totalAmount").text(total.toFixed(2));
}
