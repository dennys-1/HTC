# hotel-santa-ursula-II
Rama Freddy
Remover la carpeta Migrations

Cambiar la cadena de conexion

dotnet ef migrations add Initial --context EsportshubApi.Models.ApplicationDbContext -o YourFolderPath

example:

dotnet ef migrations add OtherSchema --context hotel_santa_ursula_II.Data.ApplicationDbContext -o "G:\git 2021-so\hotel-santa-ursula-II\Data\Migrations"

dotnet ef database update
G:\git 2021-so\hotel-santa-ursula-II\Data

## Módulo de viajes tipo inDriver

Se añadió un conjunto de endpoints REST para gestionar una experiencia de transporte colaborativo similar a inDriver. Los controladores expuestos desde `api/` permiten crear pasajeros, conductores y solicitudes de viaje, así como actualizar el estado del trayecto en tiempo real.

### Endpoints principales

| Recurso | Método | Descripción |
|---------|--------|-------------|
| `api/riders` | `GET` | Listado de pasajeros registrados. |
| `api/riders` | `POST` | Registrar un nuevo pasajero. |
| `api/drivers` | `GET` | Listado de conductores disponibles y su geolocalización. |
| `api/drivers/{id}/availability` | `PUT` | Actualizar disponibilidad y posición de un conductor. |
| `api/riderequests` | `POST` | Crear una solicitud de viaje con coordenadas y tarifa estimada. |
| `api/riderequests/{id}/assign/{driverId}` | `PUT` | Asignar un conductor disponible a un viaje pendiente. |
| `api/riderequests/{id}/status` | `PUT` | Cambiar el estado del viaje (`Pending`, `Accepted`, `InProgress`, `Completed`, `Cancelled`). |

Cada viaje actualiza automáticamente la disponibilidad del conductor al ser aceptado o completado/cancelado. El modelo de datos se encuentra en la carpeta `Models` (`Driver`, `Rider`, `RideRequest` y `RideStatus`).