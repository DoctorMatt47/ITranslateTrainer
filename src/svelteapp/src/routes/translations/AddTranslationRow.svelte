<script lang="ts">
  import AppTextInput from "$lib/components/AppTextInput.svelte";
  import { translationsStore } from "$lib/stores";
  import type { PutTranslationRequest } from "$lib/services/translation-service";
  import { putTranslation } from "$lib/services/translation-service";

  let request: PutTranslationRequest = {
    originText: {
      language: "",
      value: "",
    },
    translationText: {
      language: "",
      value: "",
    },
  };

  async function addTranslationClick() {
    const addedTranslation = await putTranslation(request);
    translationsStore.update((translations) => {
      translations.push(addedTranslation);
      return translations;
    });
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
