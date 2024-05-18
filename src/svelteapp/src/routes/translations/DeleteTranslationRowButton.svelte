<script lang="ts">
  import { getContext } from "svelte";
  import type { TranslationService } from "$lib/translations/translation-service.svelte";

  let { translationId }: {
    translationId: number;
  } = $props();

  const translationService = getContext<TranslationService>("translationService");

  async function onclick() {
    await translationService.deleteTranslation(translationId);
  }
</script>

<button {onclick}>
  <i class="fa fa-trash"></i>
</button>
