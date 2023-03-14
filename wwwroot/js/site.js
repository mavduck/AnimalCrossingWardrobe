function villagerSelect(newSelectedId) {
    var input, initialSelectedElement, newSelectedElement, newSelectedElementValue, dropdownElement;

    input = document.getElementById("selectedVillagerNameInput");

    initialSelectedVillagerName = input.value;

    if(initialSelectedVillagerName != ""){
        initialSelectedElement = document.getElementById(initialSelectedVillagerName+"ListItem");
    }
    else{
        initialSelectedElement = document.getElementById("NoVillagerListItem");
    }

    if (initialSelectedElement != null) {
        initialSelectedElement.classList.remove("active");
    }

    newSelectedElement = document.getElementById(newSelectedId);
    newSelectedElement.classList.add("active");
    newSelectedElementValue = newSelectedElement.getAttribute("data-value");

    dropdownElement = document.getElementById("selectedVillagerDropdownCover");
    dropdownElement.innerHTML = newSelectedElementValue == "" ? "Choose Villager" : newSelectedElement.innerHTML;

    var images = dropdownElement.getElementsByTagName("img");

    if(images.length > 0){
        images[0].classList.remove("villager-icon-fit");
        images[0].classList.add("villager-icon-sm");
    }

    input.value = newSelectedElementValue;
}

function villagerSearch() {
    var inputElement, searchString, dropdownElement, villagerItems, i;

    inputElement = document.getElementById("villagerDropdownSearch");
    searchString = inputElement.value.toLowerCase();
    dropdownElement = document.getElementById("villagerDropdown");
    villagerItems = dropdownElement.getElementsByClassName("villagerListItem");
    
    for(i = 0; i < villagerItems.length; i++){
        var villagerItem = villagerItems[i];
        if(villagerItem.getAttribute("data-value").toLowerCase().startsWith(searchString)){
            villagerItem.style.display = "";
        }
        else{
            villagerItem.style.display = "none";
        }
    }


}