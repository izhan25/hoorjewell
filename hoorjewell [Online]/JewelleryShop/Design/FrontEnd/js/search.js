window.addEventListener('load', search);

try
{
    document.querySelector('#searchField').addEventListener('keyup', search);

}
catch(ex)
{
    console.log('Seacrh Field is not touched !');
}

function search()
{
    var searchText = document.querySelector('#searchField');
    var searchbtn = document.querySelector('#searchButton');

    try
    {
        if(searchText.value === '')
        {
            searchbtn.disabled = true;
        }
        else
        {
            searchbtn.disabled = false;
        }
    }
    catch(ex)
    {
        console.log(ex.message);
    }

    
}