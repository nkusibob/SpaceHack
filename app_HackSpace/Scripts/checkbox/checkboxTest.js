function testcheck() {
    if (!jQuery(".selected").is(":checked")) {
        alert("CheckBox not checked.");
        return false;
    }
    return true;
};