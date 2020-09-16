function populate(catSelect,formName,subSelectName,selected)
{
    subSelect = eval("document.forms." + formName + "." + subSelectName);
    subSelect.options.length = 0;
    var catNum = catSelect.options[selected].value;

    if (subTextArray[catNum] == null)
    {
        var noneOption = new Option("-- No Subcategories --", "");
        subSelect.options[0] = noneOption;
        return;
    }

    var allOption = new Option("-- Choose a Subcategory --", "");
    subSelect.options[0] = allOption;
    for (var i = 1; i < subTextArray[catNum].length; i++)
    {
        // there might be blank ones, so skip them out
        if (subTextArray[catNum][i] != null)
        {
            var tmpOption = new Option(subTextArray[catNum][i], subValueArray[catNum][i]);
            subSelect.options[i] = tmpOption;
        }
    }
}