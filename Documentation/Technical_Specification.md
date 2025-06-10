# Техническое описание

## 1. Общая информация
- **Название**: Система учёта обращений в клиентскую службу  
- **Цель**: Автоматизация процессов обработки обращений клиентов, учёта достижений и посещаемости  
- **Тип**: Desktop-приложение (WPF)  

## 2. Технологии
| Компонент       | Технология               |
|-----------------|--------------------------|
| Frontend        | WPF (.NET 8)             |
| Backend         | ADO.NET                  |
| База данных     | SQL Server 20            |
| Аутентификация | Windows Auth/SQL Auth    |

## 3. Структура проекта
ClientServiceApp/
├── Models/ # Сущности БД
│ ├── User.cs
│ ├── Achievement.cs
├── Repositories/ # Работа с данными
│ ├── UserRepository.cs
│ ├── AchievementRepository.cs
├── Services/ # Бизнес-логика
│ └── AuthService.cs
├── Views/ # Окна приложения
│ ├── LoginWindow.xaml
│ └── MainWindow.xaml
└── App.config # Конфигурация


## 4. Диаграмма компонентов
```plantuml
@startuml
package "ClientServiceApp" {
  [WPF-UI] as ui
  [ADO.NET] as ado
  [SQL Server] as db
}
ui --> ado: Запросы данных
ado --> db: SQL-запросы
@enduml
