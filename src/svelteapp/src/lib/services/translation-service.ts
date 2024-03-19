import type { ErrorResponse } from "./api-service";
import { api } from "./api-service";
import type { TextRequest, TextResponse } from "./text-service";

export interface PutTranslationRequest {
  originText: TextRequest;
  translationText: TextRequest;
}

export interface TranslationResponse {
  id: number;
  originText: TextResponse;
  translationText: TextResponse;
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

export async function deleteTranslation(id: number): Promise<void> {
  await api.delete(`${translations}/${id}`);
}

export async function importSheet(file: File): Promise<Array<TranslationResponse | ErrorResponse>> {
  const formData = new FormData();
  formData.append("sheet", file);

  let config = { headers: { "Content-Type": "multipart/form-data" } };
  const response = await api.put(`${translations}/sheet`, formData, config);

  return response.data;
}
