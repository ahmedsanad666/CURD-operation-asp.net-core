
var dataTable;

$(document).ready(function () {
    loadData();
}); 


function loadData() {
    dataTable = $('#myTable').DataTable({
        "ajax": {
            url: 'Product/getall',

        },
        "columns": [
            { "data": "name" },
            { "data": 'isbn' },
            { "data": 'price' },
            { "data": 'author' },
            { "data": 'category.name' },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <a class="btn btn-success " href="Product/Upsert?id=${data}">Edit</a>
                     <button onClick=Delete('Product/Delete/${data}')
                        class="btn btn-danger mx-2">  Deletee</button>
					
                        `;
                }
            }
        ],
        });

}   

// delete fun

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        console.log(data.success)
                    }
                    else {
                        console.log(data.success)
                    }
                }
            })
        }
    })
}

