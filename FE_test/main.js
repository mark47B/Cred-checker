let btn = document.getElementById('submit');
let URL = 'http://localhost:5211/api/Access';
document.getElementById('result-value').value = false.toString();


btn.onclick = (e) =>
{
    let req = new XMLHttpRequest();
    req.open("POST", URL, true);

    req.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

    req.onreadystatechange = function () {
        if (req.readyState == XMLHttpRequest.DONE && req.status == 200) {
            let answer = JSON.parse(req.response);
            if (typeof (answer.access) != 'undefined') {
                document.getElementById('result-value').value = answer.access.toString();
            }
        }
    }
    req.send(JSON.stringify({
        Login: document.getElementById('login').value,
        Password: document.getElementById('pass').value
    }));
};
