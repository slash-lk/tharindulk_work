(function() {
    var Question = function(question, answers, correctAnswer) {
        this.question = question;
        this.answers = answers;
        this.correctAnswer = correctAnswer;
    };
    
    
    Question.prototype.printQuestion = function() {
        console.log(this.question);
        this.answers.forEach((answer, index) => console.log(`${index}. ${answer}`));
    };
    
    Question.prototype.checkAnswer = function(userAnswer, callback) {
        var curScore;
        if (userAnswer === this.correctAnswer) {
            console.log('Correct Answer!');
            curScore = callback(true);
        }
        else {
            console.log('Wrong Answer!');
            curScore = callback(false);
        }
        this.displayScore(curScore);
    }

    Question.prototype.displayScore = function(score) {
        console.log(`Current Score ${score}`);
        console.log('========================');
    }
    
    
    var questions = [];
    
    questions[0] = new Question('What\'s the tallest mountain in the world?', ['Alps', 'Everest', 'Andees'], 1);
    
    questions[1] = new Question('Who\'s the head of the United Kingdom?', ['Queen Mary', 'Theresa May', 'Queen Elizabeth'], 2);
    
    // questions.forEach(question => {
    //     question.printQuestion();
    //     question.checkAnswer(prompt('Answer: '));  
    //     });
    var answer, random;

    // closure
    function score() {
        var sc = 0;

        return function(isCorrect) {
            if (isCorrect) {
                sc++;
            }
            return sc;
        }
    }

    //keepScore has access to sc variable
    var keepScore = score();

    function nextQuestion() {
        random = Math.floor(Math.random() * questions.length);
    
        questions[random].printQuestion();
    
        answer = prompt('Answer: ');
        
    
        if (answer !== 'exit') {
            questions[random].checkAnswer(parseInt(answer), keepScore);
            nextQuestion();
        } 
    }

    nextQuestion();
})();

