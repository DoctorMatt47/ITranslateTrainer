<script lang="ts">
  import AppTextInput from "$lib/components/AppTextInput.svelte";
  import type { PutTranslationApiRequest } from "$lib/translations/translation-api";
  import { TranslationService } from "$lib/translations/translation-service";

  let request: PutTranslationApiRequest = {
    originText: {
      language: "",
      value: "",
    },
    translationText: {
      language: "",
      value: "",
    },
  };

  const translationService = new TranslationService();

  async function addTranslationClick() {
    await translationService.addTranslation(request);
  }
</script>

<tr>
  <td></td>
  <td>
    <AppTextInput bind:value={request.originText.language} placeholder="from" />
  </td>
  <td>
    <AppTextInput bind:value={request.translationText.language} placeholder="to" />
  </td>
  <td>
    <AppTextInput bind:value={request.originText.value} placeholder="origin" />
  </td>
  <td>
    <AppTextInput bind:value={request.translationText.value} placeholder="translation" />
  </td>
  <td class="text-center align-middle">
    <button on:click={addTranslationClick}><i class="fa fa-plus-circle"></i></button>
  </td>
</tr>
