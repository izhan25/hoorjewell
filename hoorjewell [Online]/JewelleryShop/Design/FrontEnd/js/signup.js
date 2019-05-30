function signupValid()
{
    var name = document.querySelector('#sName');
    var email = document.querySelector('#sEmail');
    var contact = document.querySelector('#sContact');
    var pwd = document.querySelector('#sPwd');
    var pwd2 = document.querySelector('#sPwd2');
    var address = document.querySelector('#sAddress');
    var card = document.querySelector('#sCard');


    //validating client name
    if(name == ""){
        document.getElementById("#msgName").innerHTML = "please provide a name";
    }
    else if( !isNaN(name) ){
        document.getElementById("#msgName").innerHTML = "Numbers are not allowed here";
    }
    else if(name.length <= 2){
        document.getElementById("#msgName").innerHTML = "please provide a name that has more than three characters";
    }





}