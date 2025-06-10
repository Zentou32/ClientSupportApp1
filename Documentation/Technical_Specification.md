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
│
├── **Models/**               # Модели данных
│   ├── User.cs               # Пользователи системы
│   ├── Achievement.cs        # Достижения сотрудников
│   └── Attendance.cs         # Учёт посещаемости
│
├── **Repositories/**         # Работа с базой данных
│   ├── UserRepository.cs     # Операции с пользователями
│   ├── AchievementRepository.cs
│   └── AttendanceRepository.cs
│
├── **Services/**             # Бизнес-логика
│   ├── AuthService.cs        # Аутентификация
│   └── ReportService.cs      # Генерация отчётов
│
├── **Views/**                # Окна приложения
│   ├── LoginWindow.xaml      # Окно авторизации
│   ├── MainWindow.xaml       # Главное окно
│   └── Achievements/         # Подраздел достижений
│       ├── AddWindow.xaml
│       └── EditWindow.xaml
│
├── **Utilities/**            # Вспомогательные классы
│   ├── Validators.cs         # Валидация данных
│   └── Converters.cs         # Конвертеры для WPF
│
├── App.config                # Конфигурация приложения
└── App.xaml                  # Точка входа


## 4. Диаграмма компонентов
@startuml
package "ClientServiceApp" {
  [WPF-UI] as ui
  [ADO.NET] as ado
  [SQL Server] as db
}
ui --> ado: Запросы данных
ado --> db: SQL-запросы
@enduml

