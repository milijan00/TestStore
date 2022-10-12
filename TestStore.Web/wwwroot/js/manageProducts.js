       export function SelectedProduct(id, image){
        this.id = id;
        this.image = image;
    }
    function getProducts() {
        if(localStorage){
            var products = localStorage.getItem("products");
            if (products) {
                return JSON.parse(products);
            } else return null;
        }
    }
    export function addProductToLocalStorage(selectedProduct){
        var addedProducts = getProducts();
        if(addedProducts.length > 0){
            var product = addedProducts.filter(x => x.id == selectedProduct.id)[0];
            if(product){
                increaseQuantityByOne();            
            }else {
                addNewProduct();
            }
        }else {
            addNewProduct();
        }
        saveProductsToLocalStorage(addedProducts);

        function addNewProduct(){
            addedProducts.push({
                id : selectedProduct.id,
                image : selectedProduct.image,
                quantity: 1
            });
        }

        function increaseQuantityByOne(){
            var index = addedProducts.indexOf(product);
           addedProducts[index].quantity += 1;
        }

    }

    function saveProductsToLocalStorage(products){
        var str = "";
        var str = JSON.stringify(products);
        localStorage.setItem("products", str);
    }

    export function removeProduct(id) {
        let products = getProducts();
        let product = products.filter(el => el.id == id)[0];
        let index = products.indexOf(product);
        products.splice(index, 1);
        saveProductsToLocalStorage(products);

    }
