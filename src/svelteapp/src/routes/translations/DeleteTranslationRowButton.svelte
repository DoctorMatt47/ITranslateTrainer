<script lang="ts">
  import { TranslationService } from "$lib/translations/translation-service";

  export let translationId: number;

  const translationService = new TranslationService();

  async function deleteTranslationClick() {
    await deleteTranslation(translationId);
  }
</script>

<button on:click={deleteTranslationClick}>
  <i class="fa fa-trash"></i>
</button>
