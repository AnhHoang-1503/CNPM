export const toISOString = (date) => {
    var parts = date.split("/");
    var dateISO = new Date(Date.UTC(parts[2], parts[1] - 1, parts[0]))
        .toISOString()
        .split("T")[0];
    return dateISO;
};
