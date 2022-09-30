export var baseUrl: string;

if (process.env.NODE_ENV !== 'production') {
  baseUrl = "http://localhost:5207"
} else {
  baseUrl = "http://192.168.0.102:5207"
}
