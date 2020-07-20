# Парсер электронной таблицы
-------
**При входе проверяется наличие файла на диске D**
 * *Найден* - Парсится файл
 * *Не найден* - Происходит запрос на скачивание файла
-------
**После парсинга имеются 4 кнопки и возможность просмотра каждой угрозы отдельно, кликнув по нужной угрозе. На каждой "странице" по 20 угроз**
* *Кнопка "Сохранить"* - сохраняет распарсенную таблицу в файл с расширением csv
* *Кнопка "Следующая страница"* - перелистывает вперед
* *Кнопка "Предыдущая страница"* - перелистывает назад
* *Кнопка "Обновить"* -  обновляет локальную базу данных угроз
-------
**При выборе угрозы открывается страница с информацией об угрозе отдельно. Имеются на данной странице следующие кнопки**
* *Кнопка "Следующая запись"* - перелистывает вперед
* *Кнопка "Предыдущая запись"* - перелистывает назад
* *Кнопка "Назад"* - возрвращается на главное окно
-------
**Кнопка обновить**
* *Обновляет*
* *Пишет "Успешно", если обновлено успешно*
* *Пишет "Нечего обновлять", если обновлять нечего*
* *Пишет количество обновленных записей*
* **При нажатии на кнопку подробнее должен был показывать список обновленных записей, но что-то пошло не по плану**
