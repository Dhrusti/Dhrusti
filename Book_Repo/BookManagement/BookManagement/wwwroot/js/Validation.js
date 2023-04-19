

    function GEEKFORGEEKS(event) {
        var FName =
            document.forms.userform.FName.value;
        console.log(FName);
        var LName =
            document.forms.userform.Lname.value;
        var Mail =
            document.forms.userform.mail.value;
        var Cntnumber =
            document.forms.userform.Cnt.value;
        var Location =
            document.forms.userform.Loc.value;
        var regEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/g;  //Javascript reGex for Email Validation.
        var regPhone = /^\d{10}$/;                                        // Javascript reGex for Phone Number validation.
        var regName = /\d+$/g;                                    // Javascript reGex for Name validation
        var isvalid = true;

        if (FName == "" || regName.test(FName)) {
            window.alert("Please enter your First name properly.");
            FName.focus();
            event.preventDefault();
            isvalid = false;
        }
        if (LName == "" || regName.test(LName)) {
            window.alert("Please enter your Last name properly.");
            LName.focus();
            event.preventDefault();
            isvalid = false;
        }

        if (Mail == "" || !regEmail.test(Mail)) {
            window.alert("Please enter a valid e-mail address.");
            Mail.focus();
            event.preventDefault();
            isvalid = false;
        }

        if (Cntnumber == "") {
            alert("Please enter your Contact Number");
            Cntnumber.focus();
            event.preventDefault();
            isvalid = false;
        }

        if (!regPhone.test(Cntnumber)) {
            alert("Contact number should be only 10 digit");
            Cntnumber.focus();
            isvalid = false;

        }
        if (Location == "") {
            window.alert("Please enter your Location.");
            Location.focus();
            isvalid = false;
        }
        

        if (isvalid == true) {
            var employee =$("#userform").serialize();
            console.log(employee);
            $.ajax({
                url: '/Home/DownloadBook',
                type: "POST",
                dataType: "JSON",
                //contentType: "application/json; charset=utf-8",
                data: employee,
                success: function (json) {                    
                    if (json.isRedirect) {
                        alert("Thanks For Downloading the Book...!!!");
                        window.open("../../Files/" + json.returnmodel, "blank");
                        
                        window.location.href = json.redirectUrl;

                    }
                },
                error: function (json) {
                    alert("Error");
                    
                    console.log("error");
                }
            });
            //return false;
        }


    };

