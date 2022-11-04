 class FormBuilder {
    firstname = undefined;
    lastname = undefined;
    email = undefined;
    username = undefined;
    password = undefined;
    category = undefined;
    passwordAgain = undefined;
    brand = undefined;
    role = undefined;
     linkName = undefined;
     action = undefined;
     controller = undefined;
     specification = undefined;

     addSpecification(el) {
         this.specification = el;
         return this;
     }

     addLinkName(el) {
         this.linkName = el;
         return this;
     }

     addAction(el) {
         this.action = el;
         return this;
     }

     addController(el) {
         this.controller = el;
         return this;
     }

     addRole(element) {
         this.role = element;
         return this;
     }

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
         if (this.role) {
             values["role"] = this.role.value;
         }
         if (this.linkName) {
             values["linkName"] = this.linkName.value;
         }
         if (this.action) {
             values["action"] = this.action.value;
         }
         if (this.controller) {
             values["controller"] = this.controller.value;
         }
         if (this.specification) {
             values['specification'] = this.specification.value;
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
         if (this.role) {
             this.role.value = "";
         }
         if (this.linkName) {
             this.linkName.value = "";
         }
         if (this.action) {
             this.action.value = "";
         }
         if (this.controller) {
             this.controller.value = "";
         }
         if (this.specification) {
             this.specification.value = "";
         }
     }
}

export { FormBuilder };