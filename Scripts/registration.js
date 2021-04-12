const form = document.getElementById('form');
const username = document.getElementById('username');
const email = document.getElementById('email');
const phone = document.getElementById('phone');
const password = document.getElementById('password');
const password2 = document.getElementById('password2');


 username.addEventListener('input', e => {
	e.preventDefault();
	
	checkUserName();
});
       
      
     email.addEventListener('input', e => {
	e.preventDefault();
	
    checkEmail();       
	
});
 phone.addEventListener('input', e => {
	e.preventDefault();
	
    checkPhone();       
	
});
     password.addEventListener('input', e => {
	e.preventDefault();
	
	checkPassword();
});

       password2.addEventListener('input', e => {
	e.preventDefault();
	
	checkPassword2();
});

function checkUserName()
      {
          const usernameValue = username.value.trim();
          if(usernameValue === '') {
		setErrorFor(username, 'Username cannot be blank');
	} else {
		setSuccessFor(username);
	}
      }
  function checkEmail()
      {
          const emailValue = email.value.trim();
          
          if(emailValue === '') {
		setErrorFor(email, 'Email cannot be blank');
	} else if (!isEmail(emailValue)) {
		setErrorFor(email, 'Not a valid email');
	} else {
		setSuccessFor(email);
	}
	}
     function checkPhone()
      {
          const phoneValue = phone.value.trim();
          if(phoneValue === '') {
		setErrorFor(phone, 'Phone cannot be blank');
	} 
          else if(isNaN(phoneValue))
                  {
                  setErrorFor(phone, 'Invalid Phone Number');
                  }
           else if(! /^[0-9]{11}$/.test(phoneValue))
                  {
                  setErrorFor(phone, 'Enter 11 Digit Number');
                  }
          
          else {
		setSuccessFor(phone);
	}
      } 
      
      function checkPassword()
      {
          
	const passwordValue = password.value.trim();
	
	if(passwordValue === '') {
		setErrorFor(password, 'Password cannot be blank');
	} else {
		setSuccessFor(password);
	}
	
      }
      
      
      function checkPassword2()
      {
          const passwordValue = password.value.trim();
          const password2Value = password2.value.trim();
          if(password2Value === '') {
		setErrorFor(password2, 'Password2 cannot be blank');
	} else if(passwordValue !== password2Value) {
		setErrorFor(password2, 'Passwords does not match');
	} else{
		setSuccessFor(password2);
	}
      }
      
form.addEventListener('submit', e => {
	e.preventDefault();
	
	checkInputs();
});     

var name,emails,validPhone,pass;
function checkInputs() {
	// trim to remove the whitespaces
	const usernameValue = username.value.trim();
	const emailValue = email.value.trim();
     const phoneValue = phone.value.trim();
	const passwordValue = password.value.trim();
	const password2Value = password2.value.trim();
	
	if(usernameValue === '') {
		setErrorFor(username, 'Username cannot be blank');
        name=false;
	} else {
		setSuccessFor(username);
        name=true;
	}
	
	if(emailValue === '') {
		setErrorFor(email, 'Email cannot be blank');
        emails=false;
	} else if (!isEmail(emailValue)) {
		setErrorFor(email, 'Not a valid email');
        emails=false;
	} else {
		setSuccessFor(email);
        emails=true;
	}
    
	  if(phoneValue === '') {
		setErrorFor(phone, 'Phone cannot be blank');
          validPhone=false;
	} 
          else if(isNaN(phoneValue))
                  {
                  setErrorFor(phone, 'Invalid Phone Number');
                      validPhone=false;
                  }
           else if(! /^[0-9]{11}$/.test(phoneValue))
                  {
                  setErrorFor(phone, 'Enter 11 Digit Number');
                      validPhone=false;
                  }
          
          else {
		setSuccessFor(phone);
         validPhone=true;      
	}
    
	
	if(passwordValue === '') {
		setErrorFor(password, 'Password cannot be blank');
        pass=false;
	} else {
		setSuccessFor(password);
        pass=true;
	}
	
	if(password2Value === '') {
		setErrorFor(password2, 'Password2 cannot be blank');
        pass=false;
	} else if(passwordValue !== password2Value) {
		setErrorFor(password2, 'Passwords does not match');
        pass=false;
	} else{
		setSuccessFor(password2);
        pass=true;
	}
    
}
form.addEventListener('submit', e => {
	e.preventDefault();
	
	if(name && pass && emails)
    {
    submitData();
    }
});
      function submitData(){
            document.getElementById("form").submit();
       }

function setErrorFor(input, message) {
	const formControl = input.parentElement;
	const small = formControl.querySelector('small');
	formControl.className = 'fm error';
	small.innerText = message;
}

function setSuccessFor(input) {
	const formControl = input.parentElement;
	formControl.className = 'fm success';
}
	
function isEmail(email) {
	return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email);
}













// SOCIAL PANEL JS
const floating_btn = document.querySelector('.floating-btn');
const close_btn = document.querySelector('.close-btn');
const social_panel_container = document.querySelector('.social-panel-container');

floating_btn.addEventListener('click', () => {
	social_panel_container.classList.toggle('visible')
});

close_btn.addEventListener('click', () => {
	social_panel_container.classList.remove('visible')
});