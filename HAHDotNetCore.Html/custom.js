function uuidv4() {
   return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
     (
       +c ^
       (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (+c / 4)))
     ).toString(16)
   );
 }

 function successMessage(message) {
   Swal.fire({
     title: "Success!",
     text: message,
     icon: "success"
  });
}

 function errorMessage(message) {
   Swal.fire({
     title: "Error!",
     text: message,
     icon: "error"
  });
 }

 function confirmMessage(message) {
   let confirmMsg = new Promise(function (success, fail) {
 
     Swal.fire({
       title: "Are you sure?",
       text: message,
       icon: "warning",
       showCancelButton: true,
       confirmButtonColor: "#3085d6",
       cancelButtonColor: "#d33",
       confirmButtonText: "Yes, delete it!",
     }).then((result) => {
       if (result.isConfirmed) {
         success();
       } else {
         fail();
       }
     });
   });
   return confirmMsg;
 }

