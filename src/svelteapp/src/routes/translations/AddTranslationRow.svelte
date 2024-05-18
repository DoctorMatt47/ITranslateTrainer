<script lang="ts">
  import AppTextInput from "$lib/common/components/AppTextInput.svelte";
  import { getContext } from "svelte";
  import type { TranslationService } from "$lib/translations/translation-service.svelte";

  const translation = $state({
    originText: {
      language: "",
      value: "",
    },
    translationText: {
      language: "",
      value: "",
    },
  });

  const translationService = getContext<TranslationService>("translationService");

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
    <button onclick={addTranslationClick}><i class="fa fa-plus-circle"></i></button>
  </td>
</tr>
