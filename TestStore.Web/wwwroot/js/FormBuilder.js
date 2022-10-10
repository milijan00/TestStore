 class FormBuilder {
    firstname = undefined;
    lastname = undefined;
    email = undefined;
    username = undefined;
    password = undefined;
    category = undefined;
    passwordAgain = undefined;
    brand = undefined;

    addFirstName(element) {
        this.firstname = element;
        return this;
    }
    addLastName(element) {
        this.lastname = element;
        return this;
    }

    addEmail(element) {
        this.email = element;
        return this;
    }

    addUsername(element) {
        this.username = element;
        return this;
    }

    addPassword(element) {
        this.password = element;
        return this;
    }

    addPasswordAgain(element) {
         this.passwordAgain = element;
         return this;
     }

    addCategory(element) {
        this.category = element;
        return this;
     }

    addBrand(element) {
        this.brand = element;
        return this;
    }

     getForm() {
         let values = {};
         if (this.firstname) {
             values["firstname"] = this.firstname.value;
         }
         if (this.lastname) {
             values["lastname"] = this.lastname.value;
         }
         if (this.username) {
             values["username"] = this.username.value;
         }

         if (this.password) {
             values["password"] = this.password.value;
         }
         if (this.passwordAgain) {
             values["passwordAgain"] = this.passwordAgain.value;
         }
         if (this.email) {
             values["email"] = this.email.value;
         }
         if (this.category) {
             values["category"] = this.category.value;
         }
         if (this.brand) {
             values["brand"] = this.brand.value;
         }

         return values;
     }
     clearForm() {
         if (this.firstname) {
             this.firstname.value = "";
         }
         if (this.lastname) {
             this.lastname.value = "";
         }
         if (this.username) {
             this.username.value = "";
         }

         if (this.password) {
             this.password.value = "";
         }

         if (this.passwordAgain) {
             this.passwordAgain.value = "";
         }
         if (this.email) {
             this.email.value = "";
         }
         if (this.category) {
             this.category.value = "";
         }
         if (this.brand) {
             this.brand.value = "";
         }
     }
}

export { FormBuilder };