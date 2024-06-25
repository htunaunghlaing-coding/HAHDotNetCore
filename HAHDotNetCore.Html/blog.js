const tblBlog = "blogs";

// createBlog();
updateBlog(
  "68a8d62f-87de-4c0a-a63e-f8300caa5299",
  "test demo",
  "test demo",
  "test demo"
);

function readBlog() {
  localStorage.getItem();
}

function createBlog() {
  const blogs = localStorage.getItem(tblBlog);
  console.log(blogs);

  let lst = [];
  if (blogs !== null) {
    lst = JSON.parse(blogs);
  }

  const requestModel = {
    id: uuidv4(),
    title: "test title",
    author: "test author",
    content: "test content",
  };

  lst.push(requestModel);

  const jsonBlog = JSON.stringify(lst);
  localStorage.setItem(tblBlog, jsonBlog);
}

function updateBlog(id, title, author, content) {
  const blogs = localStorage.getItem(tblBlog);
  console.log(blogs);

  let lst = [];
  if (blogs !== null) {
    lst = JSON.parse(blogs);
  }

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

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      +c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (+c / 4)))
    ).toString(16)
  );
}
