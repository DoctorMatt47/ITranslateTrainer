<script lang="ts">
  import AppHeading from "$lib/components/AppHeading.svelte";
  import AppNumberInput from "$lib/components/AppNumberInput.svelte";
  import AppTextInput from "$lib/components/AppTextInput.svelte";
  import type { PutTestRequest } from "$lib/services/test-service";
  import { putTest } from "$lib/services/test-service";
  import { testSettingsStore, testStore } from "$lib/stores";
  import { onMount } from "svelte";
  import { goto } from "$app/navigation";
  import AppSubmitButton from "$lib/components/AppSubmitButton.svelte";

  const settings: PutTestRequest = $testSettingsStore;

  onMount(() => {
    console.log(settings);
  });

  async function start() {
    testSettingsStore.set(settings);
    const test = await putTest(settings);
    testStore.set(test);
    await goto("/quiz");
  }
</script>

<AppHeading>Settings</AppHeading>
<form on:submit={start}>
  <div class="w-1/2 mx-auto">
    <div class="grid grid-rows-4 grid-cols-2 gap-4 text-center items-center">
      <div>From:</div>
      <AppTextInput bind:value={settings.fromLanguage} placeholder="from" />
      <div>To:</div>
      <AppTextInput bind:value={settings.toLanguage} placeholder="to" />
      <div>Option count</div>
      <AppNumberInput bind:value={settings.optionCount} />
      <AppSubmitButton className="col-span-2">Start</AppSubmitButton>
    </div>
  </div>
</form>
