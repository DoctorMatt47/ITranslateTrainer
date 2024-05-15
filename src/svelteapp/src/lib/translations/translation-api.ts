import type { TextApiRequest, TextApiResponse } from "$lib/common/services/text-service";
import createApiInstance, { type ErrorResponse } from "$lib/common/services/api-service";

export type PutTranslationApiRequest = {
  originText: TextApiRequest;
  translationText: TextApiRequest;
}

export type TranslationApiResponse = {
  id: number;
  originText: TextApiResponse;
  translationText: TextApiResponse;
}


export class TranslationsApi {
  private api = createApiInstance("/translations");

  async getTranslations(): Promise<TranslationApiResponse[]> {
    return await this.api.get("");
  }

  async putTranslation(request: PutTranslationApiRequest): Promise<TranslationApiResponse> {
    return await this.api.put("", request);
  }

  async deleteTranslation(id: number): Promise<void> {
    return await this.api.delete(id.toString());
  }

  async importTranslationSheet(file: File): Promise<(TranslationApiResponse | ErrorResponse)[]> {
    const formData = new FormData();
    formData.append("sheet", file);

    let config = { headers: { "Content-Type": "multipart/form-data" } };
    return await this.api.put("sheet", formData, config);
  }
}
