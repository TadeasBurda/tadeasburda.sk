function addRow(btn) {
    var tr = $(btn).parent().parent();

    var trClone = tr.clone();

    trClone.find("td > input").val("");

    tr.after(trClone);

    reindexRows();
}

function reindexRows() {
    var rows = $("tbody > tr");
    for (var i = 0; i < rows.length; i++) {
        $(rows[i]).find("th").text(i + 1); // change row index

        $(rows[i]).find("td > input").attr("id", "Widths_" + i + "_");
        $(rows[i]).find("td > input").attr("name", "Widths[" + i + "]");
        $(rows[i]).find("td > span").attr("data-valmsg-for", "Widths_" + i + "_");
    }
}
