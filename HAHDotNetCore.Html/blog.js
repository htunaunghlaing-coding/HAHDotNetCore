const tblBlog = "blogs";
let blogId = null;

getBlogsTable();
test1();

function test() {
  let confirmMsg = new Promise(function (success, fail) {
    const result = confirm("Are you sure want to delete?");
    if (result == true) {
      success();
    } else {
      fail();
    }
  });

  confirmMsg.then(
    function (value) {
      successMessage("Success");
    },
    function (error) {
      successMessage("Fail");
    }
  );
}

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

function editBlog(id) {
  let lst = getBlogs();

  const items = lst.filter((x) => x.id === id);
  console.log(items);

  console.log(items.length);

  if (items.length == 0) {
    errorMessage("No Data Found.");
    return;
  }

  // return items[0];
  let item = items[0];

  blogId = item.id;
  $("#txtTitle").val(item.title);
  $("#txtAuthor").val(item.author);
  $("#txtContent").val(item.content);
  $("#txtTitle").focus();
}

function updateBlog(id, title, author, content) {
  let lst = getBlogs();

  const items = lst.filter((x) => x.id === id);
  console.log(items);

  console.log(items.length);

  if (items.length == 0) {
    errorMessage("No Data Found.");
    return;
  }

  const item = items[0];
  item.title = title;
  item.author = author;
  item.content = content;

  const index = lst.findIndex((x) => x.id === id);
  lst[index] = item;

  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);

  successMessage("Update Data Successfully.");
}

function deleteBlog(id) {
  // let result = confirm("Are you sure want to delete?");
  // if (!result) return;
  confirmMessage("Are you sure want to delete?").then(function (value) {
    let lst = getBlogs();

    const items = lst.filter((x) => x.id === id);
    if (items.length == 0) {
      console.log("Data Not found.");
      return;
    }

    lst = lst.filter((x) => x.id !== id);
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    successMessage("Delete Data Successfully.");
    clearControl();
    getBlogsTable();
  });
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

  if (blogId === null) {
    createBlog(title, author, content);
  } else {
    updateBlog(blogId, title, author, content);
    blogId = null;
  }
  clearControl();
  getBlogsTable();
});

function clearControl() {
  $("#txtTitle").val("");
  $("#txtAuthor").val("");
  $("#txtContent").val("");
  $("#txtTitle").focus();
}

function getBlogsTable() {
  const lst = getBlogs();
  let count = 0;
  let htmlRows = "";
  lst.forEach((item) => {
    const htmlRow = `
        <tr>
          <td>${++count}</td>
          <td>${item.title}</td>
          <td>${item.author}</td>
          <td>${item.content}</td>
          <td>
              <button type="button" class="btn btn-warning" onclick="editBlog('${
                item.id
              }')">Edit</button>
              <button type="button" class="btn btn-danger" onclick="deleteBlog('${
                item.id
              }')">Delete</button>
          </td>
        </tr>
        `;
    htmlRows += htmlRow;
  });
  $("#tbody").html(htmlRows);
}
