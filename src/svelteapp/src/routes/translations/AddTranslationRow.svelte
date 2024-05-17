<script lang="ts">
  import AppTextInput from "$lib/components/AppTextInput.svelte";
  import { type AddTranslationRequest, TranslationService } from "$lib/translations/translation-service";

  let translation = {
    originText: {
      language: "",
      value: "",
    },
    translationText: {
      language: "",
      value: "",
    },
  } as AddTranslationRequest;

  const translationService = new TranslationService();

  async function addTranslationClick() {
    await translationService.addTranslation(translation);
  }
</script>

<tr>
  <td></td>
  <td>
    <AppTextInput bind:value={translation.originText.language} placeholder="from" />
  </td>
  <td>
    <AppTextInput bind:value={translation.translationText.language} placeholder="to" />
  </td>
  <td>
    <AppTextInput bind:value={translation.originText.value} placeholder="origin" />
  </td>
  <td>
    <AppTextInput bind:value={translation.translationText.value} placeholder="translation" />
  </td>
  <td class="text-center align-middle">
    <button on:click={addTranslationClick}><i class="fa fa-plus-circle"></i></button>
  </td>
</tr>
