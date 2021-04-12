const form = document.getElementById('form');
const phone = document.getElementById('phone');
const password = document.getElementById('password');



phone.addEventListener('input', e => {
	e.preventDefault();
	
	checkPhone();
});
 
     password.addEventListener('input', e => {
	e.preventDefault();
	
	checkPassword();
});

      


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
      
      
      
form.addEventListener('submit', e => {
	e.preventDefault();
	
	checkInputs();
});     

var validPhone,pass;
function checkInputs() {
	// trim to remove the whitespaces
	
	 const phoneValue = phone.value.trim();
	const passwordValue = password.value.trim();
	
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
	
    
}
form.addEventListener('submit', e => {
	e.preventDefault();
	
	if(validPhone && pass)
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