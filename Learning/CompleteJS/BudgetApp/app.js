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
        addBtn: '.add__btn',
        incomeContainer: '.income__list',
        expensesContainer: '.expenses__list'
    };

    return {
        getInputValues: function() {
            return {
                type: document.querySelector(DOMstrings.inputType).value,  //'select' element - value either 'inc' or 'exp'
                description: document.querySelector(DOMstrings.inputDescription).value,
                value: document.querySelector(DOMstrings.inputValue).value,
            }
        },

        addListItem: function(obj, type) {
            var html, newHtml, element;

            // Create HTML string with placeholder text
            if (type === 'inc') {
                element = DOMstrings.incomeContainer;

                html = `<div class="item clearfix" id="income-%id%"><div class="item__description">%description%</div><div class="right clearfix">
                    <div class="item__value">%value%</div><div class="item__delete"><button class="item__delete--btn"><i class="ion-ios-close-outline"></i></button>
                    </div></div></div>`;
            }
            else if (type === 'exp') {
                element = DOMstrings.expensesContainer;

                html = `<div class="item clearfix" id="expense-%id%"><div class="item__description">%description%</div><div class="right clearfix">
                    <div class="item__value">%value%</div><div class="item__percentage">21%</div><div class="item__delete"><button class="item__delete--btn">
                    <i class="ion-ios-close-outline"></i></button></div></div></div>`;
            }

            // Replace the placehodler text with some actual data
            newHtml = html.replace('%id%', obj.id);
            newHtml = newHtml.replace('%description%', obj.description);
            newHtml = newHtml.replace('%value%', obj.value);

            // Insert the HTML in to the DOM
            document.querySelector(element).insertAdjacentHTML('beforeend', newHtml);
        },


        clearFields: function() {
            var fields = document.querySelectorAll(DOMstrings.inputDescription + ',' + DOMstrings.inputValue);

            // convert list to an array
            var fieldArray = Array.prototype.slice.call(fields);

            fieldArray.forEach(element => {
                element.value = "";
            });

            fieldArray[0].focus();
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
        UICtrl.addListItem(newItem, input.type);

        // 4. Clear the fields
        UICtrl.clearFields();

        // 5. Calculate the budget

        // 6. Display the budget on the UI
    };

    return {
        init: function() {
            console.log('Application has started!');
            setupEventListeners();
        }
    }
    


})(budgetController, UIController);

controller.init();