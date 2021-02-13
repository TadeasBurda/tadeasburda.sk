function addRow(btn) {
    var tr = $(btn).parent().parent(); // Return the row that contains the button.

    // Clone the row and clear the input.
    var trClone = tr.clone();
    trClone.find("td > input").val("");

    tr.after(trClone); // Insert a clone after the original line.

    // Corrects the numbering of rows in the table.
    var rows = $("tbody tr > th");
    for (var i = 0; i < rows.length; i++) {
        $(rows[i]).text(i + 1);
    }
}

function covertAndDownload(fileName) {
    if (fileName == null) {
        alert("Musíš nahrať obrázok.");
    }
    else {
        // Create a array from the values in the form.
        var inputValues = $("tbody").find("input").map(function () {
            return parseInt($(this).val());
        }).toArray();

        $.post("/api/ConvertImgToWebP", { fileName: fileName, widths: inputValues })
            .done(function (result) {
                window.open(window.location.origin + result); // Open link in new window and download file.
            })
            .fail(function (xhr, status, error) {
                alert("Niečo sa pokazilo. Skúste to ešte raz, alebo kontaktujte admina stránky. ");
            });
    }     
}
