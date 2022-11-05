import { FormBuilder } from "/js/FormBuilder.js";
class Validator {
    result = {};
    regexes = {};
    errors = {};
    builder = null;
	constructor() {
        this.builder = new FormBuilder();
        this.errors = {
            email: "At least 5 and maximum 100 characters. It has to contain @ character",
            password: "At least one letter and number. Minimum length is 8 and maximum is 16.",
            passwordAgain: "At least one letter and number. Minimum length is 8 and maximum is 16.",
            firstname : "No numbers, no special characters, first letter is capitalized." ,
            lastname : "No numbers, no special characters, first letter is capitalized." ,
            username : "At least 3 characters and maximum is 20." ,
            category : "At least 2 characters and maximum is 19." ,
            brand : "At least 2 characters and maximum is 19." ,
            role : "At least 2 characters and maximum is 19." ,
            action : "At least 2 characters and maximum is 19." ,
            linkName : "At least 2 characters and maximum is 19." ,
            controller : "At least 2 characters and maximum is 19." ,
            usecase: "No numbers of special characters. Minimal length is 5 and maximum is 60 characters.",
            specification: "Maximum length is 30, minimal is 3."
        };
		this.regexes = {
            email: /^(?=.{5,100}$)[a-zA-ZšđžćčŠĐŽĆČ0-9.!#$%&'*+/=? ^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/,
            password: /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,16}$/,
            firstname : /^[A-ZŠĐŽĆČ][a-zšđžćč]{1,14}(\s[A-ZŠĐŽĆČ][a-zšđžćč]{1,14}){0,1}$/,
            lastname: /^[A-ZŠĐŽĆČ][a-zšđžćč]{1,14}(\s[A-ZŠĐŽĆČ][a-zšđžćč]{1,14}){0,1}$/,
            username: /^(?=.{3,20}$)[a-zA-Z0-9._]+$/,
            category: /^[A-Z][a-z]{2,19}$/,
            brand: /^[A-Z][a-z]{2,19}$/,
            role: /^[A-Z][a-z]{2,19}$/,
            action: /^[A-Z][a-z]{2,19}$/,
            linkName: /^[A-Z][a-z\s]{2,19}$/,
            controller: /^[A-Za-z]{2,19}$/,
            usecase: /^[A-Za-z]{5,60}$/,
            specification: /^[A-Z][a-z\s-\/_]{2,19}$/
        };
    }
    validateForm() {
        this.result = {
            isValid: true,
            errors: {}
        };
        const form = this.builder.getForm();
        for (let k in this.regexes) {
            for (let j in form) {
                if (k == j) {
                    if (!form[j].match(this.regexes[k])) {
                        this.result.errors[j] = this.errors[j] ;
                        this.result.isValid = false;
                    }
                } else if (k == "password" && j == "passwordAgain") {
                    if (!form[j].match(this.regexes.password) || form.password != form.passwordAgain) {
                        this.result.errors["passwordAgain"] =   this.errors.passwordAgain;
                        this.result.isValid = false;
                    }
                }
            }
        }
        return this.result;
    }
    clearForm() {
        this.builder.clearForm();
    }

    clearResults() {
        this.result = {
            isValid: true,
            errors: {}
        }
    }
    validateValue(value, key) {
        if (this.regexes[key]) {
            if (!value.match(this.regexes[key])) {
                // validation successfull
                this.result.errors[key] = this.errors[key];
                this.result.isValid = false;
                return false;
            }
            return true;
        } else if (key == "newPassword") {
            if (!value.match(this.regexes.password)) {
                this.result.errors.newPassword = this.errors.password;
                this.result.isValid = false;
                return false;
            }
            return true;
        }
    }
}
    

export { Validator };

const hideErrorBlock = (id)=>{
    $(id).addClass("d-none");
    $(id).removeClass("d-block");
}

 const hideErrorBlocks = (ids)=>{
        for(let id of ids){
           hideErrorBlock(id);
        }
    };

const setTextValueForErrorBlock = (id, value)=>{
        $(id + " p").text(value);
    }

const displayErrorMessage = (id)=>{
        $(id).addClass("d-block");
        $(id).removeClass("d-none");
    }
const displayErrorMessages = (errors) => {
    for (var e in errors) {
        let blockId ="#"+ e + "Error";
        displayErrorMessage(blockId);
        setTextValueForErrorBlock(blockId, errors[e]);
    }
}
export default {
    hideErrorBlocks,
    displayErrorMessages
};
