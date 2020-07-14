
function openFileUploader(x) {
    document.getElementById('FileUpload' + x).click();
}

$(document).ready(function () {

    $(".about").click(function () {
        $("#aboutDiv").toggle();
    });

    $(".howToPlay").click(function () {
        $("#howToPlayDiv").toggle();
    });

    $(".closeAbout").click(function () {
        $("#aboutDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });

    $(".closeHowToPlay").click(function () {
        $("#howToPlayDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });

    $("#editorName").keyup(function () {
        $("#tryAgain").text("");
        if ($("#editorName").val().length > 0 && $("#editorPassword").val().length > 0) {
            document.getElementById("loginButton").disabled = false;
            document.getElementById("loginButtontooltip").style.visibility = "hidden";
        }
        else {
            document.getElementById("loginButton").disabled = true;
            document.getElementById("loginButtontooltip").style.visibility = "visible";

        }
    });

    $("#editorPassword").keyup(function () {
        $("#tryAgain").text("");
        if ($("#editorName").val().length > 0 && $("#editorPassword").val().length > 0) {
            document.getElementById("loginButton").disabled = false;
            document.getElementById("loginButtontooltip").style.visibility = "hidden";
        }
        else {
            document.getElementById("loginButton").disabled = true;
            document.getElementById("loginButtontooltip").style.visibility = "visible";
        }
    });

    $(".CharacterCount").keydown(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });
    //בהקלדה בתיבת הטקסט
    $(".CharacterCount").keyup(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });
    //בהעתקה של תוכן לתיבת הטקסט
    $(".CharacterCount").on("paste", function () {
        checkCharacter($(this));//קריאה לפונקציה שבודקת את מספר התווים
    });

    $("#backTo").click(function () {
        document.getElementById("goBackButton").click();
    });

    
    $("#questionText").keydown(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });
    //בהקלדה בתיבת הטקסט
    $("#questionText").keyup(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });

    $("#questionText").on("paste", function () {
        checkCharacter($(this));
    });

    $("#questionText").focus(function () {
        var LableToShow = $(this).attr("CharacterForLabel");
        $("#" + LableToShow).attr("style", "visibility: visible;");
    });

    $("#questionText").focusout(function () {
        var LableToShow = $(this).attr("CharacterForLabel");
        $("#" + LableToShow).attr("style", "visibility: hidden;");
    });

    $("#questionText").ready(function () {
        if (document.getElementById("questionText") != null) {
            if ($("#questionText").val().length <= 0) {
                document.getElementById("ImageButton1").disabled = true;
                document.getElementById("answerImage1").disabled = true;
                document.getElementById("Label1").className = "aspNetDisabled answerLabelClass";

                document.getElementById("ImageButton2").disabled = true;
                document.getElementById("answerImage2").disabled = true;
                document.getElementById("Label2").className = "aspNetDisabled answerLabelClass";
                $("#answer2tooltip").attr("style", "visibility: hidden;");

                document.getElementById("ImageButton3").style.visibility = "hidden";
                document.getElementById("answerImage3").style.visibility = "hidden";
                document.getElementById("Label3").style.visibility = "hidden";
                document.getElementById("answer3tooltip").style.visibility = "hidden";

                document.getElementById("ImageButton4").style.visibility = "hidden";
                document.getElementById("answerImage4").style.visibility = "hidden";
                document.getElementById("Label4").style.visibility = "hidden";
                document.getElementById("answer4tooltip").style.visibility = "hidden";

                document.getElementById("ImageButton5").style.visibility = "hidden";
                document.getElementById("answerImage5").style.visibility = "hidden";
                document.getElementById("Label5").style.visibility = "hidden";
                document.getElementById("answer5tooltip").style.visibility = "hidden";

                document.getElementById("ImageButton6").style.visibility = "hidden";
                document.getElementById("answerImage6").style.visibility = "hidden";
                document.getElementById("Label6").style.visibility = "hidden";
                document.getElementById("answer6tooltip").style.visibility = "hidden";
            }
            else {
                if (document.getElementById("answerDelete1") == null) {
                    document.getElementById("ImageButton2").disabled = true;
                    document.getElementById("answerImage2").disabled = true;
                    document.getElementById("Label2").className = "aspNetDisabled answerLabelClass";
                }
            }
        }
    });


    $("#ImageButton1").ready(function () {
        if (document.getElementById("answer1Textbox") != null && document.getElementById("ImageButton2") != null && $("#answer1Textbox").val().length <= 0) {
            document.getElementById("ImageButton2").disabled = true;
            document.getElementById("answerImage2").disabled = true;
            document.getElementById("Label2").className = "aspNetDisabled answerLabelClass";
        }
    });


    $("#answersList_0").ready(function () {
        if (document.getElementById("answersList_0") != null && document.getElementById("answer1Textbox") != null) {
        var check = $("#answersList_0").attr("checked");
         var check1 = $("#answer1Textbox");
            if (check != "checked" && check1.val().length<= 0) {
                document.getElementById("answersList_0").disabled = true;
                document.getElementById("ImageButton2").disabled = true;
                document.getElementById("answerImage2").disabled = true;
                document.getElementById("Label2").className = "aspNetDisabled answerLabelClass";
            }
        }
    });

    $("#answersList_1").ready(function () {
        if (document.getElementById("answersList_1") != null && document.getElementById("answer2Textbox") != null) {
            var check = $("#answersList_1").attr("checked");
            var check1 = $("#answer2Textbox");
            if (check != "checked" &&check1.val().length <= 0) {
                document.getElementById("answersList_1").disabled = true;
                document.getElementById("ImageButton3").disabled = true;
                document.getElementById("answerImage3").disabled = true;
                document.getElementById("Label3").className = "aspNetDisabled answerLabelClass";
            }
        }
    });

    $("#answersList_2").ready(function () {
        if (document.getElementById("answersList_2") != null && document.getElementById("answer3Textbox") != null) {
            var check = $("#answersList_2").attr("checked");
            var check1 = $("#answer3Textbox");
            if (check != "checked"&&check1.val().length <= 0) {
                document.getElementById("answersList_2").disabled = true;
                document.getElementById("ImageButton4").disabled = true;
                document.getElementById("answerImage4").disabled = true;
                document.getElementById("Label4").className = "aspNetDisabled answerLabelClass";
            }
        }
    });


    $("#answersList_3").ready(function () {
        if (document.getElementById("answersList_3") != null && document.getElementById("answer4Textbox") != null) {
            var check = $("#answersList_3").attr("checked");
            var check1 = $("#answer4Textbox");
            if (check != "checked" && check1.val().length <= 0) {
                document.getElementById("answersList_3").disabled = true;
                document.getElementById("ImageButton5").disabled = true;
                document.getElementById("answerImage5").disabled = true;
                document.getElementById("Label5").className = "aspNetDisabled answerLabelClass";
            }
        }
    });

    $("#answersList_4").ready(function () {
        if (document.getElementById("answersList_4") != null && document.getElementById("answer5Textbox") != null) {
            var check = $("#answersList_4").attr("checked");
            var check1 = $("#answer5Textbox");
            if (check != "checked" && check1.val().length <= 0) {
                document.getElementById("answersList_4").disabled = true;
                document.getElementById("ImageButton6").disabled = true;
                document.getElementById("answerImage6").disabled = true;
                document.getElementById("Label6").className = "aspNetDisabled answerLabelClass";
            }
        }
    });
    
    $("#answersList_5").ready(function () {
        if (document.getElementById("answersList_5") != null && document.getElementById("answer6Textbox") != null) {
            var check = $("#answersList_5").attr("checked");
            var check1 = $("#answer6Textbox");
            if (check != "checked" && check1.val().length <= 0) {
                document.getElementById("answersList_5").disabled = true;
            }
        }
    });
    
    for (i = 1; i <= 6; i++) {

        $(".ansText" + i + "Class").keydown(function () {
            checkCharacter($(this));
        });
        $(".ansText" + i + "Class").keyup(function () {
            checkCharacter($(this));
        });
        $(".ansText" + i + "Class").on("paste", function () {
            checkCharacter($(this));
        });
        $(".ansText" + i + "Class").focus(function () {
            var LableToShow = $(this).attr("CharacterForLabel");
            $("#" + LableToShow).attr("style", "visibility: visible;");
        });

        $(".ansText" + i + "Class").focusout(function () {
            var LableToShow = $(this).attr("CharacterForLabel");
            $("#" + LableToShow).attr("style", "visibility: hidden;");
        });

    }



    //פונקציה שמקבלת את תיבת הטקסט שבה מקלידים ובודקת את מספר התווים
    function checkCharacter(myTextBox) {
        //משתנה למספר התווים הנוכחי בתיבת הטקסט
        var countCurrentC = myTextBox.val().length;
        //משתנה המקבל את שם הלייבל המקושר לאותה תיבת טקסט 
        var LableToShow = myTextBox.attr("CharacterForLabel");
       $("#" + LableToShow).attr("style", "visibility: visible;");
        //משתנה המכיל את מספר התווים שמוגבל לתיבה זו
        var CharacterLimit = myTextBox.attr("CharacterLimit");
         //משתנה המקבל את שם הלייבל להודעה המקושר לאותה תיבת טקסט 
        var message = myTextBox.attr("CharacterForLabel2");
         //משתנה המקבל את שם הלייבל להודעה המקושר לאותה תיבת טקסט 
        var enableThis = myTextBox.attr("enableThis");
        //בדיקה האם ישנה חריגה במספר התווים
        if (countCurrentC >= CharacterLimit) {
            //מחיקת התווים המיותרים בתיבה
            myTextBox.val(myTextBox.val().substring(0, CharacterLimit));
            //עדכון של מספר התווים הנוכחי
            countCurrentC = CharacterLimit;
            document.getElementById(LableToShow).style.color = "red";
            $("#" + message).text("*הגעת לספר התווים המקסימלי*");
        }
        //הדפסה כמה תווים הוקלדו מתוך כמה
        $("#" + LableToShow).text(countCurrentC + "/" + CharacterLimit);
        $("#" + message).text("");

        if (document.getElementById("saveButton") != null) {

        if (document.getElementById("saveButtontooltip") != null) {
        var toolTip = document.getElementById("saveButtontooltip").className;
        }
        if (myTextBox.className != "CharacterCount") {
            if (myTextBox.val().length != 0) {

                if (document.getElementById("answersList_" + (enableThis - 2))!=null) {
                document.getElementById("answersList_" + (enableThis - 2)).disabled = false;}
                myTextBox.attr("style", "border-color: gray;");

                var enabel = "yes";
                // מודא שכל התשובות האחרות מלאות לפני שמאפשר הוספת תשובה
                for (i = 1; i <= 6; i++) {
                    if (document.getElementById("answer"+i+"Textbox")!=null)
                    {
                        if ($(".ansText" + i + "Class").val().length == 0) {
                        enabel = "no";
                        }
                    }
                }

                for (i = 6; i >= 1; i--) {
                    if (enabel == "yes") {
                    if (document.getElementById("Label" + i).style.visibility == "visible") {
                        document.getElementById("ImageButton" + i).disabled = false;
                        document.getElementById("answerImage" + i).disabled = false;
                        document.getElementById("Label" + i).className = "answerLabelClass";
                        document.getElementById("answer" + i + "tooltip").style.visibility = "hidden";
                        break;
                        }
                    }
                }
                if (document.getElementById("saveButtontooltip") != null && $("#questionText").val().length != 0) {
                    if (document.getElementById("answerDelete2") != null) {
                        if ($("#answersList_0").is(":checked") || $("#answersList_1").is(":checked") || $("#answersList_2").is(":checked") || $("#answersList_3").is(":checked") || $("#answersList_4").is(":checked") || $("#answersList_5").is(":checked")) {
                            document.getElementById("saveButton").disabled = false;
                            document.getElementById("saveButtontooltip").style.visibility = "hidden";
                        }
                        else {
                            document.getElementById("saveButtontooltip").style.visibility = "visible";
                            $("#saveButton" + toolTip).text("חובה לסמן תשובה נכונה");
                            document.getElementById("saveButton").disabled = true;}
                    }
                }
                if (event.currentTarget.className == "questImageFalse" || event.currentTarget.className == "questImageTrue") {
                    document.getElementById("Label" + enableThis).className = "answerLabelClass";
                    document.getElementById("ImageButton" + enableThis).disabled = false;
                    document.getElementById("answerImage" + enableThis).disabled = false;
                    document.getElementById("answer1tooltip").style.visibility = "hidden";
                    if (document.getElementById("answerDelete2") != null) {
                        if ($("#answersList_0").is(":checked") || $("#answersList_1").is(":checked") || $("#answersList_2").is(":checked") || $("#answersList_3").is(":checked") || $("#answersList_4").is(":checked") || $("#answersList_5").is(":checked")) {
                            document.getElementById("saveButton").disabled = false;
                            document.getElementById("saveButtontooltip").style.visibility = "hidden";
                        }
                        else {
                            document.getElementById("saveButtontooltip").style.visibility = "visible";
                            $("#saveButton" + toolTip).text("חובה לסמן תשובה נכונה");
                            document.getElementById("saveButton").disabled = true;
                        }

                    }
                    else {
                        if (document.getElementById("answerDelete1") == null) {
                            document.getElementById("answer1tooltip").style.visibility = "hidden";
                            $("#saveButton" + toolTip).text("שאלה חייבת להכיל שתי תשובות לפחות");
                        }
                    }
                }

            }
                
            else {
                myTextBox.attr("style", "border-color: red;");

                if (event.currentTarget.className == "questImageFalse" || event.currentTarget.className == "questImageTrue") {
                    document.getElementById("ImageButton" + enableThis).disabled = true;
                    document.getElementById("answerImage" + enableThis).disabled = true;
                    document.getElementById("Label" + enableThis).className = "aspNetDisabled answerLabelClass";
                    document.getElementById("answer" + enableThis + "tooltip").style.visibility = "visible";
                    document.getElementById("saveButtontooltip").style.visibility = "visible";
                    document.getElementById("saveButton").disabled = true;
                    $("#saveButton" + toolTip).text("לא ניתן לשמור שאלה ללא טקסט");
                }
                else {
                    for (i = 6; i >= 1; i--) {
                        if (document.getElementById("answerImage" + i).style.visibility == "visible") {
                            document.getElementById("ImageButton" + i).disabled = true;
                            document.getElementById("answerImage" + i).disabled = true;
                            document.getElementById("Label" + i).className = "aspNetDisabled answerLabelClass";
                            document.getElementById("answer" + i + "tooltip").style.visibility = "visible";
                            break;
                        }
                    }
                    document.getElementById("answersList_" + (enableThis - 2)).disabled = true;
                }
            }
            }
        }
        else {
            var myToolTip = myTextBox.attr("myToolTip");
            var myBtn = myTextBox.attr("myBtn");
            var myBtntooltip = myTextBox.attr("myBtnToolTip");
            var myCheckBox = myTextBox.attr("myCheckBox");
            var myCheckBoxtooltip = myTextBox.attr("myCheckBoxTooltip");
            var myTime = myTextBox.attr("myTime");
            var myTimetooltip = myTextBox.attr("myTimetooltip");
            var myDelete = myTextBox.attr("myDelete");
            var myDeletetooltip = myTextBox.attr("myDeletetooltip");

            // var myCheckBoxtooltipStat = $("#" + myCheckBoxtooltip).text;
            if (myTextBox.val().length == 0) {
                myTextBox.attr("style", "border: 1px solid red;");
                $("#" + myToolTip).text("לא ניתן לערוך משחק ללא שם");
                $("#" + myBtntooltip).text("שם המשחק חייב להכיל לפחות 1 תווים");
                $("#" + myCheckBoxtooltip).text("לא ניתן לשנות מצב פרסום במשחק ללא שם");
                $("#" + myTimetooltip).text("לא ניתן לערוך זמן במשחק ללא שם");
                $("#" + myDeletetooltip).text("לא ניתן למחוק משחק ללא שם");
                document.getElementById(myBtn).disabled = true;
                document.getElementById(myBtn).style.opacity = "0.5";
                document.getElementById(enableThis).disabled = true;
                document.getElementById(enableThis).style.opacity = "0.5";
                document.getElementById(myCheckBox).disabled = true;
                document.getElementById(myTime).style.opacity = "0.5";
                document.getElementById(myTime).disabled = true;
                document.getElementById(myDelete).style.opacity = "0.5";
                document.getElementById(myDelete).disabled = true;
                document.getElementById(myBtn).style.cursor = "not-allowed";
                document.getElementById(enableThis).style.cursor = "not-allowed";
                document.getElementById(myCheckBox).style.cursor = "not-allowed";
                document.getElementById(myTime).style.cursor = "not-allowed";
                document.getElementById(myDelete).style.cursor = "not-allowed";
            }
            else {
                $("#" + myToolTip).text("עריכת שאלות המשחק");
                myTextBox.attr("style", "border: none;");
                $("#" + myBtntooltip).text("עדכן שם משחק");
                $("#" + myTimetooltip).text("מספר שניות לשאלה");
                $("#" + myDeletetooltip).text("מחיקת משחק מהמאגר");
                document.getElementById(myBtn).disabled = false;
                document.getElementById(myBtn).style.opacity = "1";
                document.getElementById(enableThis).disabled = false;
                document.getElementById(enableThis).style.opacity = "1";
                document.getElementById(myTime).style.opacity = "1";
                document.getElementById(myTime).disabled = false;
                document.getElementById(myDelete).style.opacity = "1";
                document.getElementById(myBtn).style.cursor = "pointer";
                document.getElementById(enableThis).style.cursor = "pointer";
                document.getElementById(myCheckBox).style.cursor = "pointer";
                document.getElementById(myTime).style.cursor = "pointer";
                document.getElementById(myDelete).style.cursor = "auto";

                $("#" + myCheckBoxtooltip).text("פרסם משחק");
                document.getElementById(myCheckBox).disabled = false;
            }

        }
       
    }

    //לאחר שלחצנו על התמונה שרצינו לבחור - תמונה מספר אחד
    $("#FileUpload0").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#ImageButtonquestImg').attr('src', e.target.result);
                document.getElementById('ImageButtonquestImg').onload += imageUpload(0); 
                document.getElementById('ImageButtonquestImg').className = "answerImageClassTrue";
            }
            reader.readAsDataURL(this.files[0]);
        }
    });

    $("#FileUpload1").change(function () {
            if (this.files && this.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#answerImage1').attr('src', e.target.result);
                    document.getElementById('answerImage1').onload += imageUpload(1);
                    document.getElementById('answerImage1').className = "answerImageClassTrue";
                }
                reader.readAsDataURL(this.files[0]); 
            }
    });

    $("#FileUpload2").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#answerImage2').attr('src', e.target.result);
                document.getElementById('answerImage2').onload += imageUpload(2);
                document.getElementById('answerImage2').className = "answerImageClassTrue";
            }
            reader.readAsDataURL(this.files[0]);
        }
    });

    $("#FileUpload3").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#answerImage3').attr('src', e.target.result);
                document.getElementById('answerImage3').onload += imageUpload(3);
                document.getElementById('answerImage3').className = "answerImageClassTrue";
            }
            reader.readAsDataURL(this.files[0]);
        }
    });

    $("#FileUpload4").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#answerImage4').attr('src', e.target.result);
                document.getElementById('answerImage4').onload += imageUpload(4);
                document.getElementById('answerImage4').className = "answerImageClassTrue";
            }
            reader.readAsDataURL(this.files[0]);
        }
    });

    $("#FileUpload5").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#answerImage5').attr('src', e.target.result);
                document.getElementById('answerImage5').onload += imageUpload(5);
                document.getElementById('answerImage5').className = "answerImageClassTrue";
            }
            reader.readAsDataURL(this.files[0]);
        }
    });

    $("#FileUpload6").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#answerImage6').attr('src', e.target.result);
                document.getElementById('answerImage6').onload += imageUpload(6);
                document.getElementById('answerImage6').className = "answerImageClassTrue";
            }
            reader.readAsDataURL(this.files[0]);
        }
    });

    var x, i, j, l, ll, selElmnt, a, b, c;

    x = document.getElementsByClassName("custom-select");
    l = x.length;
    for (i = 0; i < l; i++) {
        selElmnt = x[i].getElementsByTagName("select")[0];
        var itemsId = selElmnt.id;
        var itemsrow = (selElmnt.id).charAt((selElmnt.id).length-1);
        var gameCode = document.getElementById(itemsId).attributes["theItemId"];
        ll = selElmnt.length;
        a = document.createElement("DIV");
        a.setAttribute("class", "select-selected");
        a.id = itemsrow + "Time";
        a.innerHTML = selElmnt.options[selElmnt.selectedIndex].innerHTML;
        x[i].appendChild(a);
        x[i].id = itemsrow + "CostumeSelect";
        b = document.createElement("DIV");
        b.setAttribute("class", "select-items select-hide");
        for (j = 0; j < ll; j++) {
            c = document.createElement("DIV");
            c.id = itemsrow;
            c.setAttribute("theItemId", gameCode);
            c.innerHTML = selElmnt.options[j].innerHTML;
            if (selElmnt.options[j].innerHTML == a.innerHTML) {
            c.removeAttribute("class");
            c.setAttribute("class", "same-as-selected");
            }
            c.addEventListener("click", function (e) {
                var timeText = this.innerHTML;
                if (timeText == "בלי הגבלה") {
                    timeText = "no";
                }
                document.getElementById("GridView1_hiddenForTime" + timeText + "_" + this.id).click();
                var y, i, k, s, h, sl, yl;       
                s = this.parentNode.parentNode.getElementsByTagName("select")[0];
                var q = s.selectedIndex;
                s[q].removeAttribute("selected");
                sl = s.length;
                h = this.parentNode.previousSibling;
                for (i = 0; i < sl; i++) {
                    if (s.options[i].innerHTML == this.innerHTML) {
                        s.selectedIndex = i;
                        s[i].setAttribute("selected", "selected");
                        s.removeAttribute("theItemValue");
                        s.setAttribute("theItemValue", s.options[i].innerHTML);
                        h.innerHTML = this.innerHTML;
                        y = this.parentNode.getElementsByClassName("same-as-selected");
                        yl = y.length;
                        for (k = 0; k < yl; k++) {
                            y[k].removeAttribute("class");
                        }
                        this.setAttribute("class", "same-as-selected");
                        break;
                    }
                }
                h.click();

            });
            b.appendChild(c);
        } 
        x[i].appendChild(b);

        a.addEventListener("click", function (e) {
            if (!this.disabled) {
            e.stopPropagation();
            closeAllSelect(this);
            this.nextSibling.classList.toggle("select-hide");
            this.classList.toggle("select-arrow-active");}
        });
        
        //a.addEventListener("mouseover", function (e) {

        //    e.stopPropagation();
        //    closeAllSelect(this);
        //    this.nextSibling.classList.toggle("select-hide");
        //    this.classList.toggle("select-arrow-active");
        //});
    }
    function closeAllSelect(elmnt) {

        var x, y, i, xl, yl, arrNo = [];
        x = document.getElementsByClassName("select-items");
        y = document.getElementsByClassName("select-selected");
        xl = x.length;
        yl = y.length;
        for (i = 0; i < yl; i++) {
            if (elmnt == y[i]) {
                arrNo.push(i)
            } else {
                y[i].classList.remove("select-arrow-active");
            }
        }
        for (i = 0; i < xl; i++) {
            if (arrNo.indexOf(i)) {
                x[i].classList.add("select-hide");
            }
        }
    }

    document.addEventListener("click", closeAllSelect);
    

});

function saveButtonActive() {
    document.getElementById("saveButton").Enabled = "true";
}

function deleteAnswer(num) {
    document.getElementsByClassName("answer" + num + "TextBox").Enabled = "false";
    document.getElementById("saveButton").Enabled = "false"; 
}

function imageUpload(x)
{
    document.getElementById("ansbutImage" + x).click();
    
    if (x != 0) {
        document.getElementById("answer" + (x + 1) + "tooltip").style.visibility = "visible";
        document.getElementById("ImageButton" + (x + 1)).disabled = false;
        document.getElementById("answerImage" + (x + 1)).disabled = false;
        document.getElementById("Label" + (x + 1)).disabled = false;
    }
    }



