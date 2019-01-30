
function myCart(productName, productPrice, productImage, productId)
{
    var qty = 1;

    var item = {productId, productName, productPrice, productImage, qty};

    var ls = window.localStorage.getItem('cart');

    if(ls === null)
    {
        //if local storage is empty
        var cart = [];
        cart.push(item);

        window.localStorage.setItem('cart', JSON.stringify(cart));
    }
    else
    {
        // if local storage is not empty
        var cart = JSON.parse( window.localStorage.getItem('cart') );

        var isOk = false;
        for(var i = 0 ; i < cart.length ; i++)
        {
            if(cart[i].productId == productId)
            {
                cart[i].qty = cart[i].qty + 1;
                isOk = true;
                window.localStorage.setItem('cart', JSON.stringify(cart));
                break;
            }
            
        }

        if(!isOk)
        {
            cart.push(item);
            window.localStorage.setItem('cart', JSON.stringify(cart));
        }

    }

}

function emptyCart()
{
    var cart = [];
    
    window.localStorage.setItem('cart', JSON.stringify(cart));
}