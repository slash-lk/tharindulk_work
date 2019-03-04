// BUDGET CONTROLLER module
var budgetController = (function() {


})();


// UI CONTROLLER module
var UIController = (function() {
    var DOMstrings = {
        inputType: '.add__type',
        inputDescription: '.add__description',
        inputValue: '.add__value',
        addBtn: '.add__btn'
    };

    return {
        getInputValues: function() {
            return {
                type: document.querySelector(DOMstrings.inputType).value,  //'select' element - value either 'inc' or 'exp'
                description: document.querySelector(DOMstrings.inputDescription).value,
                value: document.querySelector(DOMstrings.inputValue).value,
            }
        },

        getDOMstrings: function() {
            return DOMstrings;
        }
    };

})();


// GLOBAL APP CONTROLLER module
var controller = (function(budgetCtrl, UICtrl) {

    var setupEventListeners = function() {

        var DOMstrings = UICtrl.getDOMstrings();

        document.querySelector(DOMstrings.addBtn).addEventListener('click', ctrlAddItem);

        document.addEventListener('keypress', (event) => {
    
            //'which' for backward compatibility
            if ((event.keyCode === 13) || (event.which === 13)) {
                ctrlAddItem();
            }
        });
    }

    var ctrlAddItem = function() {

        // 1. Get the field input data
        var input = UICtrl.getInputValues();
        console.log(input);


        // 2. Add the item to the budget controller

        // 3. Add the item to the UI

        // 4. Calculate the budget

        // 5. Display the budget on the UI
    };

    return {
        init: function() {
            console.log('Application has started!');
            setupEventListeners();
        }
    }
    


})(budgetController, UIController);

controller.init();