var apiTask;

enter.onclick = async function () {
    var xhrMessage;
    const xhr = new XMLHttpRequest();
    let response, form, result;
    switch (apiTask) {
        case "getbit":
            xhrMessage = `a=${document.getElementById("a").value}&k=${document.getElementById("k").value}`;
            break;
        case "changebit":
            xhrMessage = `a=${document.getElementById("a").value}&k=${document.getElementById("k").value}`;
            break;
        case "swapbit":
            xhrMessage = `a=${document.getElementById("a").value}&i=${document.getElementById("i").value}&j=${document.getElementById("j").value}`;
            break;
        case "zerosmallbits":
            xhrMessage = `a=${document.getElementById("a").value}&m=${document.getElementById("m").value}`;
            break;
        case "gluebits":
            xhrMessage = `a=${document.getElementById("a").value}&i=${document.getElementById("i").value}&l=${document.getElementById("len").value}`;
            break;
        case "midlebits":
            xhrMessage = `a=${document.getElementById("a").value}&i=${document.getElementById("i").value}&l=${document.getElementById("len").value}`;
            break;
        case "swapbytes":
            xhrMessage = `a=${document.getElementById("a").value}&i=${document.getElementById("i").value}&j=${document.getElementById("j").value}`;
            break;
        case "maxdegreebin":
            xhrMessage = `a=${document.getElementById("a").value}`;
            break;
        case "insidediapason":
            xhrMessage = `x=${document.getElementById("x").value}`;
            break;
        case "autoxor":
            xhrMessage = `x=${document.getElementById("x").value}&p=${document.getElementById("p").value}`;
            break;
        case "cycleshiftleft":
            xhrMessage = `a=${document.getElementById("a").value}&p=${document.getElementById("p").value}&n=${document.getElementById("n").value}`;
            break;
        case "cycleshiftright":
            xhrMessage = `a=${document.getElementById("a").value}&p=${document.getElementById("p").value}&n=${document.getElementById("n").value}`;
            break;
        case "transposbits":
            xhrMessage = `a=${document.getElementById("a").value}&t=${document.getElementById("p").value}`;
            break;
        case "vernam":
            response = await fetch('http://localhost:4040/api/vernam', {
                method: 'POST',
                body: new FormData(formElem)
            });
            form = document.getElementById("notification");
            if (response.ok) {
                result = await response.text();
                form.innerHTML = `<center><b>Результат: </b> <a target="_blank" href=\"${result}\">${result}</a></center>`;
            }
            else
            {
                result = await response.json();
                form.innerHTML = `<center><b>${result.Error}</b></center>`;
            }
            break;
        case "des":
            document.getElementById("enter").value = "Обрабатываем...";
            document.getElementById("enter").setAttribute("disabled", "")
            response = await fetch('http://localhost:4040/api/des', {
                method: 'POST',
                body: new FormData(formElem)
            });
            form = document.getElementById("notification");
            if (response.ok)
            {
                result = await response.text();
                form.innerHTML = `<center><b>Результат: </b> <a target="_blank" href=\"${result}\" download>${result}</a></center>`;
                document.getElementById("enter").value = "Выполнить";
                document.getElementById("enter").removeAttribute("disabled", "");
            }
            else
            {
                result = await response.json();
                form.innerHTML = `<center><b>${result.Error}</b></center>`;
                document.getElementById("enter").value = "Выполнить";
                document.getElementById("enter").removeAttribute("disabled", "");
            }
            //var elements = document.getElementsByName("mode");
            //var mode;
            //for (var i = 0; i < elements.length; i++)
            //{
            //    if (elements[i].checked)
            //    {
            //        mode = elements[i].value;
            //        break;
            //    }

            //}
            //xhrMessage = `message=${document.getElementById("message").value}&key=${document.getElementById("key").value}&mode=${mode}&c0=${document.getElementById("c0").value}&decode=${document.getElementById("decode").checked}`;
            break;
        case "rc4":
            response = await fetch('http://localhost:4040/api/rc4', {
                method: 'POST',
                body: new FormData(formElem)
            });
            form = document.getElementById("notification");
            if (response.ok) {
                result = await response.text();
                form.innerHTML = `<center><b>Результат: </b> <a target="_blank" href=\"${result}\" download>${result}</a></center>`;
            }
            else {
                result = await response.json();
                form.innerHTML = `<center><b>${result.Error}</b></center>`;
            }
            break;
    }
    if (apiTask == "vernam") return;
    if (apiTask == "des") return;
    if (apiTask == "rc4") return;
    xhr.open("POST", "http://localhost:4040/api/" + apiTask);
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send(xhrMessage);
    xhr.onload = function () {
        let form = document.getElementById("notification");
        if (xhr.status == 200)
            form.innerHTML = `<center><b>Результат:</b> ${xhr.response}</center>`;
        else
            form.innerHTML = `<center><b>${JSON.parse(xhr.response)["Error"]}</b></center>`;

    }
}

function task1(e) {
    var e = e || event;
    var target = event.target || event.srcElement;
    var value = target.value;

    var form = document.getElementById("form");
    var info = document.getElementById("info");
    main.removeAttribute("style");
    document.getElementById("notification").innerHTML = "";
    switch (value) {
        case "11A":
            apiTask = "getbit";
            form.innerHTML = `
                    <h6></h6>
                    <label>a = </label>
                    <input id="a" class="inp" /><br><br>
                    <label>k = </label>
                    <input id="k" class="inp" /><br><br> `;
            info.innerHTML = `С клавиатуры вводится 32-х разрядное целое число 𝑎 в двоичной системе счисления.<br>
                              Вывести 𝑘-ый бит числа 𝑎. Номер бита предварительно запросить у пользователя.`;
            break;
        case "11B":
            apiTask = "changebit";
            form.innerHTML = `
                    <h6></h6>
                    <label>a = </label>
                    <input id="a" class="inp" /><br><br>
                    <label>k = </label>
                    <input id="k" class="inp" /><br><br>
                    
                `;
            info.innerHTML = `С клавиатуры вводится 32-х разрядное целое число 𝑎 в двоичной системе счисления.<br>
                              Установить/снять 𝑘-ый бит числа 𝑎`;
            break;
        case "11C":
            apiTask = "swapbit";
            form.innerHTML = `
                    <h6></h6>
                    <label>a: </label>
                    <input id="a" class="inp" /><br><br>
                    <label>i: </label>
                    <input id="i" class="inp" /><br><br>
                    <label>j: </label>
                    <input id="j" class="inp" /><br><br>`;
            info.innerHTML = `С клавиатуры вводится 32-х разрядное целое число 𝑎 в двоичной системе счисления.<br>
                             Поменять местами 𝑖−ый и 𝑗−ый биты в числе 𝑎. Числа 𝑖 и 𝑗 предварительно запросить у пользователя.`;
            break;
        case "11D":
            apiTask = "zerosmallbits";
            form.innerHTML = `<h6></h6>
                    <label>a: </label>
                    <input id="a" class="inp" /><br><br>
                    <label>m: </label>
                    <input id="m" class="inp" /><br><br>`;
            info.innerHTML = `С клавиатуры вводится 32-х разрядное целое число 𝑎 в двоичной системе счисления.<br>
                              Обнулить младшие 𝑚 бит.`;
            break;

        case "12A":
            apiTask = "gluebits";
            form.innerHTML = `<h6></h6>
                    <label>a: </label>
                    <input id="a" class="inp" /><br><br>
                    <label>i: </label>
                    <input id="i" class="inp" /><br><br>
                    <label>len: </label>
                    <input id="len" class="inp" /><br><br>`;
            info.innerHTML = `«Склеить» первые 𝑖 битов с последними 𝑖 битами из целого числа длиной 𝑙𝑒𝑛 битов.<br>
                                Пусть есть 12-ти разрядное целое число, представленное в двоичной системе счисления 100011101101.<br>
                                «Склеим» первые 3 и последние 3 бита, получим 100101.`;
            break;
        case "12B":
            apiTask = "midlebits";
            form.innerHTML = `<h6></h6>
                    <label>a: </label>
                    <input id="a" class="inp" /><br><br>
                    <label>i: </label>
                    <input id="i" class="inp" /><br><br>
                    <label>len: </label>
                    <input id="len" class="inp" /><br><br>`;
            info.innerHTML = `Получить биты из целого числа длиной 𝑙𝑒𝑛 битов, находящиеся между первыми 𝑖 битами и последними 𝑖 битами.Пример.<br>
            Пусть есть 12 - ти разрядное целое число, представленное в двоичной системе счисления 100011101101.<br>
            Получим биты находящиеся между первыми 3 и последними 3 битами: 011101`;
            break;
        case "13":
            apiTask = "swapbytes";
            form.innerHTML = `
                    <h6></h6>
                    <label>a: </label>
                    <input id="a" class="inp" /><br><br>
                    <label>i: </label>
                    <input id="i" class="inp" /><br><br>
                    <label>j: </label>
                    <input id="j" class="inp" /><br><br>
                    
                `;
            info.innerHTML = `Поменять местами байты в заданном 32 - х разрядном целом числе.Перестановка задается пользователем.`;
            break;
        case "14":
            apiTask = "maxdegreebin";
            form.innerHTML = `
                    <h6></h6>
                    <label>a: </label>
                    <input id="a" class="inp" /><br><br>
          
                `;
            info.innerHTML = `Найти максимальную степень 2, на которую делится данное целое число.`;
            break;
        case "15":
            apiTask = "insidediapason";
            form.innerHTML = `
                    <h6></h6>
                    <label>x: </label>
                    <input id="x" class="inp" /><br><br>
          
                `;
            info.innerHTML = `Пусть 𝑥 целое число.Найти такое 𝑝, что 2^(p)≤𝑥≤2^(p+1)`; 
            break;
        case "16":
            apiTask = "autoxor";
            form.innerHTML = `
                    <h6></h6>
                    <label>x: </label>
                    <input id="x" class="inp" /><br><br>
                    <label>p: </label>
                    <input id="p" class="inp" /><br><br>
          
                `;
            info.innerHTML = `Дано 2^p разрядное целое число. «Поксорить» все биты этого числа друг с другом. <br>
                              Пример.101110001 → 1; 11100111 → 0.`
            break;
        case "17A":
            apiTask = "cycleshiftleft";
            form.innerHTML = `
                    <h6></h6>
                    <label>a: </label>
                    <input id="a" class="inp" /><br><br>
                    <label>p: </label>
                    <input id="p" class="inp" /><br><br>
                    <label>n: </label>
                    <input id="n" class="inp" /><br><br>
          
                `;
            info.innerHTML = "Написать макросы циклического сдвига в 2^p разрядном целом числе на 𝑛 бит влево.";
            break;
        case "17B":
            apiTask = "cycleshiftright";
            form.innerHTML = `
                    <h6></h6>
                    <label>a: </label>
                    <input id="a" class="inp" /><br><br>
                    <label>p: </label>
                    <input id="p" class="inp" /><br><br>
                    <label>n: </label>
                    <input id="n" class="inp" /><br><br>
          
                `;
            info.innerHTML = "Написать макросы циклического сдвига в 2^p разрядном целом числе на 𝑛 бит вправо.";
            break;
            
            break;
        case "18":
            apiTask = "transposbits";
            form.innerHTML = `
                    <h6></h6>
                    <label>a: </label>
                    <input id="a" class="inp" /><br><br>
                    <label>p: </label>
                    <input id="p" class="inp" /><br><br>
          
                `;
            info.innerHTML = `Дано 𝑛 битовое данное. Задана перестановка бит (1, 8, 23, 0, 16, ... ). Написать<br>
                              функцию, выполняющую эту перестановку. Пример. 10101110 → 11110001.<br>
                              Биты, переставлены в соответствии с перестановкой (5,3,7,1,4,0,6,2).`;
            break;
        case "19":
            apiTask = `vernam`;
            form.innerHTML = `
                    <form id="formElem">
                        <h6></h6>
                        <label>Сообщение: </label>
                        <input type="file" name="message">
                        <label>Ключ: </label>
                        <input type="file" name="key">
                    </form>
                `;
            info.innerHTML = `Разработать приложение, шифрующее и дешифрующее файл с помощью алгоритма Вернама`;
            break;
        case "110":
            apiTask = `des`;
            form.innerHTML = `
                <form id="formElem">
                        <h6></h6>
                        <label>Сообщение: </label>
                        <input type="file" name="message">
                        <label>Ключ(8 символов): </label>
                        <input type="text" name="key"><br><br>
                        <label>Вектор с0 (для CBC, CFB, OFB режимов) (8 символов): </label>
                        <input type="text" name="c0"><br><br>
                        <label>Режим работы: </label>
                        <input name="mode" type="radio" value="ecb"> ECB
                        <input name="mode" type="radio" value="cbc"> CBC
                        <input name="mode" type="radio" value="cfb"> CFB
                        <input name="mode" type="radio" value="ofb"> OFB<br><br>
                        <input type="checkbox" name="decode"> Расшифровать<br><br>
                </form>
                `;
            info.innerHTML = `Разработайте приложение, обеспечивающее безопасность данных на основе алгоритма DES.Примечание.<br>
                              В приложении реализовать возможность выбора режима работы алгоритма.`;
            break;

        case "111":
            apiTask = `rc4`;
            form.innerHTML = `<form id="formElem">
                        <h6></h6>
                        <label>Сообщение: </label>
                        <input type="file" name="message">
                        <label>Ключ: </label>
                        <input type="file" name="key">
                    </form> `;
            info.innerHTML = `Реализуйте алгоритм RC4`;
    }
}

lab1.onclick = task1;