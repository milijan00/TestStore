export function SelectedProduct(id, image, price){
    this.id = id;
    this.image = image;
   this.price = price;
}
function getProducts() {
    if(localStorage){
        var products = localStorage.getItem("products");
        if (products) {
            return JSON.parse(products);
        } else return null;
    }
}
export function mapProductsForSending() {
    return getProducts().map(function (el) {
        return { id: el.id, unitPrice: parseFloat(el.price), quantity: el.quantity };
    });
}
export function addProductToLocalStorage(selectedProduct){
    var addedProducts = getProducts();
    if(addedProducts.length > 0){
        var product = addedProducts.filter(x => x.id == selectedProduct.id)[0];
        if (product) {
            increaseQuantityByOne({ addedProducts: addedProducts, product: product });
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
            image: selectedProduct.image,
            price : selectedProduct.price,
            quantity: 1
        });
    }


}

export function decreaseQuantityByOne(opts){
    let products = getProducts();
    let product = products.filter(el => el.id == opts.id)[0];
    var index = products.indexOf(product);
    products[index].quantity -= 1;
    if (products[index].quantity < 1) {
        products[index].quantity = 1;
    }
    saveProductsToLocalStorage(products);
}
export function increaseQuantityByOne(opts) {
    if (opts.addedProducts && opts.product) {
        var index = opts.addedProducts.indexOf(opts.product);
        opts.addedProducts[index].quantity += 1;
    }
    else if (opts.id) {
        let products = getProducts();
        let product = products.filter(el => el.id == opts.id)[0];
        var index = products.indexOf(product);
        products[index].quantity += 1;
        saveProductsToLocalStorage(products);
    }
}

export function totalPrice(){
    var total = 0;
    for (let p of getProducts()) {
        total += Number(p.price) * Number(p.quantity);
    }
    return total;
}
export function saveProductsToLocalStorage(products){
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
