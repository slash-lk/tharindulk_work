// BUDGET CONTROLLER module
var budgetController = (function() {
    var Expense = function(id, description, value) {
        this.id = id;
        this.description = description;
        this.value = value;
    }

    var Income = function(id, description, value) {
        this.id = id;
        this.description = description;
        this.value = value;
    }

    var data = {
        allitems: {
            inc: [],
            exp: []
        },
        totals: {
            inc: 0,
            exp: 0
        }
    }

    return {
        addItem: function(type, desc, val) {
            var ID, newItem;

            ID = 0;

            // Create new ID
            if (data.allitems[type].length > 0) {
                ID = data.allitems[type][data.allitems[type].length - 1].id + 1;
            }

            // Create new item based on 'inc' or 'exp' type
            if (type === 'exp') {
                newItem = new Expense(ID, desc, val);
            }
            else if (type === 'inc') {
                newItem = new Income(ID, desc, val);
            }

            // push it into our data structure
            data.allitems[type].push(newItem);

            //return the new element
            return newItem;
        }, 

        testing: function() {
            console.log(data);
        }
    }

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
        var input, newItem;

        // 1. Get the field input data
        var input = UICtrl.getInputValues();

        // 2. Add the item to the budget controller
        newItem = budgetCtrl.addItem(input.type, input.description, input.value);

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