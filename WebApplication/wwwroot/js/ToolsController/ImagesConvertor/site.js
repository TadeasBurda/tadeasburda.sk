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
        $.get("/api/ConvertImgToWebP", { fileName: fileName, widths: [0, 0] });
    }     
}
