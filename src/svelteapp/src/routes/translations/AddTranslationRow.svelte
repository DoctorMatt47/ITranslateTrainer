<script lang="ts">
  import AppTextInput from "../../lib/components/AppTextInput.svelte";
  import { translationsStore } from "../../lib/stores";
  import type { PutTranslationRequest } from "../../lib/services/translation-service";
  import { putTranslation } from "../../lib/services/translation-service";

  let request: PutTranslationRequest = {
    firstText: "",
    firstLanguage: "",
    secondLanguage: "",
    secondText: "",
  };

  async function add() {
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
    <AppTextInput bind:value={request.firstLanguage} placeholder="from" />
  </td>
  <td>
    <AppTextInput bind:value={request.secondLanguage} placeholder="to" />
  </td>
  <td>
    <AppTextInput bind:value={request.firstText} placeholder="original" />
  </td>
  <td>
    <AppTextInput bind:value={request.secondText} placeholder="translation" />
  </td>
  <td class="text-center align-middle">
    <button on:click={add}>
      <i class="fa fa-plus-circle"></i>
    </button>
  </td>
</tr>

<style>
    .align-middle {
        vertical-align: middle;
    }
</style>
