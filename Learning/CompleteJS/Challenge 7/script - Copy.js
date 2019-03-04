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
    
    Question.prototype.checkAnswer = function(userAnswer) {
        if (userAnswer === this.correctAnswer) {
            console.log('Correct Answer!');
            return 1;
        }
        else {
            console.log('Wrong Answer!');
            return 0;
        }
    }
    
    
    var questions = [];
    
    questions[0] = new Question('What\'s the tallest mountain in the world?', ['Alps', 'Everest', 'Andees'], '1');
    
    questions[1] = new Question('Who\'s the head of the United Kingdom?', ['Queen Mary', 'Theresa May', 'Queen Elizabeth'], '2');
    
    // questions.forEach(question => {
    //     question.printQuestion();
    //     question.checkAnswer(prompt('Answer: '));  
    //     });
    var answer, random, score;
    
    score = 0;
    
    do {
        random = Math.floor(Math.random() * questions.length);
    
        questions[random].printQuestion();
    
        answer = prompt('Answer: ');
        score += questions[random].checkAnswer(answer);
    
        console.log(`Score: ${score}`);
    } while (answer !== 'exit');
})();

