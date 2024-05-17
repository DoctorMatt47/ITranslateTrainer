import { type TextApiRequest, textApiResponseSchema } from "$lib/common/services/text-service";
import createApiInstance, { type ErrorResponse } from "$lib/common/services/api-service";
import { z } from "zod";

export type PutTranslationApiRequest = {
  originText: TextApiRequest;
  translationText: TextApiRequest;
}

const translationApiResponseSchema = z.object({
  id: z.number(),
  originText: textApiResponseSchema,
  translationText: textApiResponseSchema,
});

export type TranslationApiResponse = z.infer<typeof translationApiResponseSchema>;

export class TranslationApi {
  private api = createApiInstance("/translations");

  async getTranslations(): Promise<TranslationApiResponse[]> {
    const response = await this.api.get("");
    return z.array(translationApiResponseSchema).parse(response.data);
  }

  async putTranslation(request: PutTranslationApiRequest): Promise<TranslationApiResponse> {
    const response = await this.api.put("", request);
    return translationApiResponseSchema.parse(response.data);
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
