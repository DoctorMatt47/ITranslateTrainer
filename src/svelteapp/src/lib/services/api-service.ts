import axios from "axios";
import {dev} from "$app/environment";

const baseUrl = dev
    ? "http://localhost:5207"
    : "https://api.ourdomain.com";

export interface ErrorResponse {
    errorMessage: string;
}

export const api = axios.create({
    baseURL: baseUrl + "/api",
    timeout: dev ? undefined : 10000,
    headers: {
        "Content-Type": "application/json",
    },
});
