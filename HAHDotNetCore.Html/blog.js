const tblBlog = "blogs";

getBlogsTable();

//createBlog();
// updateBlog(
//   "68a8d62f-87de-4c0a-a63e-f8300caa5299",
//   "test demo",
//   "test demo",
//   "test demo"
// );
//deleteBlog("5449f29a-e34c-427c-9a2f-8b88e40ddb6c");

function readBlog() {
  let lst = getBlogs();
  console.log(lst);
}

function createBlog(title, author, content) {
  let lst = getBlogs();

  const requestModel = {
    id: uuidv4(),
    title: title,
    author: author,
    content: content,
  };

  lst.push(requestModel);

  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);

  successMessage("Saving Success.");
  clearControl();
}

function updateBlog(id, title, author, content) {
  let lst = getBlogs();

  const items = lst.filter((x) => (x.id = id));
  console.log(items);

  console.log(items.length);

  if (items.length == 0) {
    console.log("No Data Found");
    return;
  }

  const item = items[0];
  item.title = title;
  item.author = author;
  item.content = content;

  const index = lst.findIndex((x) => (x.id = id));
  lst[index] = item;

  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);
}

function deleteBlog(id) {
  let lst = getBlogs();

  const items = lst.filter((x) => x.id == id);
  if (items.length == 0) {
    console.log("Data Not found.");
    return;
  }

  lst = lst.filter((x) => (x.id = !id));
  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);
}

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      +c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (+c / 4)))
    ).toString(16)
  );
}

function getBlogs() {
  const blogs = localStorage.getItem(tblBlog);
  console.log(blogs);

  let lst = [];
  if (blogs !== null) {
    lst = JSON.parse(blogs);
  }
  return lst;
}

$("#btnSave").click(function () {
  const title = $("#txtTitle").val();
  const author = $("#txtAuthor").val();
  const content = $("#txtContent").val();
  createBlog(title, author, content);
});

function successMessage(message) {
  alert(message);
}

function errorMessage(message) {
  alert(message);
}

function clearControl() {
  $("#txtTitle").val(" ");
  $("#txtAuthor").val(" ");
  $("#txtContent").val(" ");
  $("#txtTitle").focus();
}

function getBlogsTable() {
  const lst = getBlogs();
  let count = 0;
  let htmlRows = '';
  lst.forEach((item) => {
      const htmlRow = `
      <tr>
         <td>${++count}</td>
         <td>${item.title}</td>
         <td>${item.author}</td>
         <td>${item.content}</td>
      </tr>
      `;
    htmlRows += htmlRow;
  });
  $('#tbody').html(htmlRows);
}
