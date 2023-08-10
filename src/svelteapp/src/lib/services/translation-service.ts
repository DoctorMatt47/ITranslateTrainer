import type { ErrorResponse } from "./api-service";
import { api } from "./api-service";
import type { TextResponse } from "./text-service";

export interface PutTranslationRequest {
  firstText: string,
  firstLanguage: string,
  secondText: string,
  secondLanguage: string
}

export interface TranslationResponse {
  id: number;
  first: TextResponse;
  second: TextResponse;
}

const translations = "/translations";

export async function getTranslations(): Promise<TranslationResponse[]> {
  const response = await api.get(translations);
  return response.data;
}

export async function putTranslation(request: PutTranslationRequest): Promise<TranslationResponse> {
  const response = await api.put(translations, request);
  return response.data;
}

export async function importSheet(file: File): Promise<Array<TranslationResponse | ErrorResponse>> {
  const formData = new FormData();
  formData.append("sheet", file);

  let config = { headers: { "Content-Type": "multipart/form-data" } };
  const response = await api.put(`${translations}/sheet`, formData, config);

  return response.data;
}
